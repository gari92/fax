#version 330
in vec3 VertexPosition;
in vec2 VertexTex;
in vec3 Norm;

out vec3 Position;
out vec3 Normal1;
out vec2 tex;
uniform int t;
uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;
uniform mat3 Normal;


void main()
{

mat4 ModelViewMatrix=View*Model;
mat4 MVP;
MVP=Projection*View*Model;

tex=VertexTex.xy;
tex.y=1.0-VertexTex.y;

Normal1 = normalize( Normal * Norm);//Prosledi normalu
Position = vec3( ModelViewMatrix *vec4(VertexPosition,1.0) );//Prosledi poziciju
gl_Position = MVP * vec4(VertexPosition,1.0);


}
