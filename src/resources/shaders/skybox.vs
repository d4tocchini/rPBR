#version 330

// Input vertex attributes
in vec3 vertexPosition;

// Input uniform values
uniform mat4 projection;
uniform mat4 view;

// Output vertex attributes (to fragment shader)
out vec3 fragPos;

void main()
{
    // Calculate fragment position based on model transformations
    fragPos = vertexPosition;
    
    // Remove translation from the view matrix
    mat4 rotView = mat4(mat3(view));
    vec4 clipPos = projection*rotView*vec4(vertexPosition, 1.0);

    // Calculate final vertex position
    gl_Position = clipPos.xyww;
}