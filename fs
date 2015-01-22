#version 330

uniform sampler2D texUnit;
uniform sampler2D texUnit1;
uniform sampler2D texUnit2;

uniform int t;
in vec3 Position;
in vec3 Normal1;
in vec2 tex1;
in vec4 color;


out vec4 FragColor;


struct LightInfo {
vec4 Position; // Light position in eye coords.
vec3 La; // Ambient light intensity
vec3 Ld; // Diffuse light intensity
vec3 Ls; // Specular light intensity
};
uniform LightInfo Light;

struct MaterialInfo {
vec3 Ka; // Ambient reflectivity
vec3 Kd; // Diffuse reflectivity
vec3 Ks; // Specular reflectivity
float Shininess; // Specular shininess factor
};
uniform MaterialInfo Material;


void main() {


if(t==0){
vec3 n=normalize(Normal1);
vec3 s = normalize(Light.Position.xyz - Position);
vec3 v = normalize(-Position);
vec3 r = reflect( -s, n );

vec3 ambient = Light.La * Material.Ka;

float sDotN = max( dot(s,n), 0.0 );

vec3 diffuse = Light.Ld * Material.Kd * sDotN;
vec3 spec = vec3(0.0);

spec = Light.Ls * Material.Ks *pow( max( dot(r,v), 0.0 ), Material.Shininess );

vec4 LightIntensity = vec4((ambient + diffuse),1.0)+ vec4(spec,1.0);


//vec4 LightIntensity = vec4((ambient + diffuse),1.0)*texture2D(texUnit,tex1)+ vec4(spec,1.0);//Sa teksturom


gl_FragColor=LightIntensity;

}

else if(t==1){

gl_FragColor =color*texture2D(texUnit1, tex1);//Prva tekstura Goraud boja*tekstura

gl_FragColor =vec4(gl_FragCoord.z,gl_FragCoord.z,gl_FragCoord.z,1.0);//Druga tekstura depth vrednost
}

else if(t==2){
vec3 n=normalize(Normal1);//Interpolacijom preko fragmenta nece biti 1
vec3 n1;
n1=n*0.5+0.5;//Konvertujemo normalu o raspon 0-1 (inace je od -1 do +1)



gl_FragColor =vec4(n1,1.0);//treca tekstura normala
}



else{


vec3 n=normalize(Normal1);
vec3 s = normalize(Light.Position.xyz - Position);
vec3 v = normalize(-Position);

vec3 r = reflect( -s, n );

vec3 ambient = Light.La * Material.Ka;
float sDotN = max( dot(s,n), 0.0 );
vec3 diffuse = Light.Ld * Material.Kd * sDotN;
vec3 spec = vec3(0.0);

spec = Light.Ls * Material.Ks *pow( max( dot(r,v), 0.0 ), Material.Shininess );

vec4 LightIntensity =vec4((ambient + diffuse),1.0);

vec4 fc;
if(sDotN>0.95){
 fc=1.0*LightIntensity;
}
else if(sDotN>0.8) fc=0.8*LightIntensity;
else if(sDotN>0.7) fc=0.7*LightIntensity;
else if(sDotN>0.6) fc=0.6*LightIntensity;
else if(sDotN>0.5) fc=0.5*LightIntensity;
else if(sDotN>0.4) fc=0.4*LightIntensity;
else if(sDotN>0.25) fc=0.25*LightIntensity;
else  fc=0.1*LightIntensity;

gl_FragColor=fc;

}





}
