#version 330 core

in vec3 aPosition;// for each triangle 
in vec3 aNormal;  // for each triangle 
in vec2 aTexCoord;// for each triangle 

out vec2 texCoord;

uniform mat4 model;     // for each Model
uniform mat4 view;      // for each Model
uniform mat4 projection;// for each Model

void main(void)
{
    texCoord = aTexCoord;
    gl_Position = projection * view * model * vec4(aPosition, 1.0);
}