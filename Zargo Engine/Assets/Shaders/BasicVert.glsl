#version 330 core

layout(location = 0) in vec3 aPosition;// for each triangle 
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

in vec3 worldSpacePosition;

out vec3 FragPos;
out vec3 Normal;
out vec2 texCoord;

void main(void)
{
    Normal = aNormal;
        
    FragPos = vec3(model * vec4(aPosition, 1.0));

    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1) *  model * view * projection;
}