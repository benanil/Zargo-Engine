#version 330 core

layout(location = 0) in vec3 aPosition;// for each triangle 
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

in vec3 worldSpacePosition;

out vec3 pworldSpacePosition;
out vec3 pLocalPosition;
out vec3 pnormal;
out vec2 texCoord;

const vec3 LightPosition = vec3(0.0,100.0,50.0);

void main(void)
{
    pworldSpacePosition = worldSpacePosition;
    
    pnormal = aNormal;
    pLocalPosition = aPosition.xyz;

    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1) *  model * view * projection;
}