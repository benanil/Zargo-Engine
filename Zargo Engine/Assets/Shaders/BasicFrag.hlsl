#version 330

out vec4 outputColor;

in vec3 pworldSpacePosition;
in vec3 pLocalPosition;
in vec3 pnormal;
in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = vec4(texture(texture0, texCoord).xyz,1);
}