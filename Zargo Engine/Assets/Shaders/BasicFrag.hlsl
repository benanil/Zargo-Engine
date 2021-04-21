#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = vec4(texCoord, 0.0, 0.0);//vec4(texture(texture0, texCoord).xyz,1);
}