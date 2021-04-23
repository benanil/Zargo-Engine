#version 330

out vec4 outputColor;

in vec3 pworldSpacePosition;
in vec3 pLocalPosition;
in vec3 Normal;
in vec2 texCoord;
in vec3 FragPos;

uniform sampler2D texture0;

void main()
{
    outputColor = vec4(texture(texture0, texCoord).xyz,1.0);
}