#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = vec4(1, 1, 1, 1);//texture(texture0, texCoord) ;
}