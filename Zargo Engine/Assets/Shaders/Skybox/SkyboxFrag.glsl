#version 330 core
out vec4 FragColor;

in vec3 TexCoords;

uniform samplerCube texture0;

void main()
{
    FragColor = textureCube(texture0, TexCoords) / 2;
}