#version 330 core

layout(location = 0) in vec3 aPosition;// for each triangle 
layout(location = 1) in vec3 aNormal;  // for each triangle 
layout(location = 2) in vec2 aTexCoord;// for each triangle 

out vec2 texCoord;

uniform mat4 model;     // for each Model
uniform mat4 view;      // for each Model
uniform mat4 projection;// for each Model

void main(void)
{
    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
}