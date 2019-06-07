Shader "tp2/alg1"
{
	Properties
	{
		_T ("a", 2D) = "white" {}
		_hoja("b", 2D) = "white" {}
		_Size("c", Range(1,96)) = 1 //nao e utilizado
		_l("d", Vector) = (1,1,1,0)
		_p("g", Vector) = (1,1,1,0)
		_umbali("h", Range(0,5)) = 1
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geo

			#include "UnityCG.cginc"

			struct vert_in
			{
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct geo_in 
			{
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct frag_in
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			uniform sampler2D _T;
			uniform float _Size;
			uniform Texture2D _hoja;
			uniform SamplerState sampler_hoja;
			uniform float3 _l;
			uniform float3 _p;
			uniform float _umbali;

			geo_in vert (vert_in i)
			{
				geo_in o = (geo_in)0;
				o.pos = i.pos;
				o.normal = i.normal;
				o.uv = i.uv;
				return o;
			}

			[maxvertexcount(21)] //tag que indica a quantidade maxima de vertices
			void geo(triangle geo_in i[3], inout TriangleStream<frag_in> triStream)
			{
				//i vai ser intrepertado como um triangulo dai o array ter 3 pontos
				//triStream e a estrura que vai ser usada para processar as primitivas e po-las no fragment shader

				float d1 = length(i[0].pos.xyz - i[1].pos.xyz); //distancia entre o triangulo 0 e o 1.
				float d2 = length(i[0].pos.xyz - i[2].pos.xyz); //distancia entre o triangulo 0 e o 2.
				float d3 = length(i[1].pos.xyz - i[2].pos.xyz); //distancia entre o triangulo 1 e o 2.
				float3 n, pp;
				float2 uv;
					//media entre as normais, os uvs e posiçoes
					//procura o maior lado 
				if (d1 > d2) 
				{
					if (d1 > d3) 
					{

						n = normalize((i[0].normal + i[1].normal) / 2); 
						uv = (i[0].uv + i[1].uv) / 2;
						pp = (i[0].pos.xyz + i[1].pos.xyz) * 0.5;
					}
					else 
					{
						n = normalize((i[2].normal + i[1].normal) / 2);
						uv = (i[2].uv + i[1].uv) / 2;
						pp = (i[2].pos.xyz + i[1].pos.xyz) * 0.5;
					}
				}
				else 
				{
					if (d2 > d3) 
					{
						n = normalize((i[0].normal + i[2].normal) / 2);
						uv = (i[0].uv + i[2].uv) / 2;
						pp = (i[0].pos.xyz + i[2].pos.xyz) * 0.5;
					}
					else 
					{
						n = normalize((i[2].normal + i[1].normal) / 2);
						uv = (i[2].uv + i[1].uv) / 2;
						pp = (i[2].pos.xyz + i[1].pos.xyz) * 0.5;
					}
				}

				float disp;
				float dd = length(pp - _p); //distancia entre pp e _p
				float ddc = length(pp - float3(0,0,0)); //distancia entre pp e 0,0
				if ( dd < _umbali) //se dd for maior que um threshold da um valor. 
				{
					disp = 0;
				}
				else
				{
					// this takes information from a texture. specifically the red part of the rgb
					// then it multiplies it with absolute of the cos of time.
					//? max(min(abs(cos(ddc*_Time.x*10))*2, 5.5), 0.0);
					disp = _hoja.SampleLevel(sampler_hoja, uv, 0).r *
								abs(cos(_Time.x)) * max(min(abs(cos(ddc*_Time.x*10))*2, 5.5), 0.0);
				}

				//A estudar a função podemos concluir que o valor retornado pelo “SampleLevel”
				//vai modificar a distância a que o deslocamento chega, enquanto que se aumentarmos a
				//distância entre a variável pp e o ponto 0,0,0 vemos que a quantidade de “saltos” vai
				//aumentar.


				float4 newp[4]; //gasto de memoria ao por uma quarta posiçao que nunca vai ser usada
				//bad programming

				//cria variaveis novas dependendo do deslocamento calculado
				newp[0] = float4(i[0].pos.xyz + n * disp, 1.0); 
				newp[1] = float4(i[1].pos.xyz + n * disp, 1.0);
				newp[2] = float4(i[2].pos.xyz + n * disp, 1.0);

				frag_in o;
				float3 newN;

				//cria as ligaçoes entre as faces descoladas e as originais
				/*for (uint it=0; it<3; it++) 
				{

					
					newN = cross(	normalize(newp[it] - i[it].pos),
									normalize(i[(it + 1) % 3].pos - i[it].pos)
								); 
					o.pos = UnityObjectToClipPos(i[it].pos);
					o.uv = i[it].uv;
					o.normal = newN;
					triStream.Append(o);
					o.pos = UnityObjectToClipPos(i[(it + 1) % 3].pos);
					o.uv = i[(it + 1) % 3].uv;
					o.normal = newN;
					triStream.Append(o);
					o.pos = UnityObjectToClipPos(newp[it]);
					o.uv = i[it].uv;
					o.normal = newN;
					triStream.Append(o);
					triStream.RestartStrip();
					

					newN = cross(	normalize(newp[it] - newp[(it + 1) % 3]),	
									normalize(i[(it + 1) % 3].pos - newp[(it + 1) % 3])
								);
					o.pos = UnityObjectToClipPos(newp[it]);
					o.uv = i[it].uv;
					o.normal = newN;
					triStream.Append(o);
					o.pos = UnityObjectToClipPos(i[(it + 1) % 3].pos);
					o.uv = i[(it + 1) % 3].uv;
					o.normal = newN;
					triStream.Append(o);
					o.pos = UnityObjectToClipPos(newp[(it + 1) % 3]);
					o.uv = i[(it + 1) % 3].uv;
					o.normal = newN;
					triStream.Append(o);
					triStream.RestartStrip();
				}*/
				
				// para cada ponto do triangulo poe a sua posiçao no mundo
				o.pos = UnityObjectToClipPos(newp[0]);
				o.uv = i[0].uv;
				o.normal = n;
				//Adds the point to the triangle
				triStream.Append(o);

				o.pos = UnityObjectToClipPos(newp[1]);
				o.uv = i[1].uv;
				o.normal = n;
				triStream.Append(o);

				o.pos = UnityObjectToClipPos(newp[2]);
				o.uv = i[2].uv;
				o.normal = n;
				triStream.Append(o);

				// "closes" the triangle, And prepares the stream for the next one
				triStream.RestartStrip();

			}
			
			float4 frag (frag_in i) : COLOR
			{
				float4 color = tex2D(_T, i.uv); //cor apartir de uma textura
				float intensity = max(dot(i.normal, normalize(_l)), 0.0); //intensidade da cor
				return color * intensity;
			}

			ENDCG
		}
	}
}
