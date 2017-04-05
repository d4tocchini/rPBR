#version 330

// Input vertex attributes (from vertex shader)
in vec3 fragPos;

// Material parameters
uniform samplerCube environmentMap;

const float PI = 3.14159265359f;

// Output fragment color
out vec4 finalColor;

void main()
{
    // The sample direction equals the hemisphere's orientation
    vec3 normal = normalize(fragPos);

    vec3 irradiance = vec3(0.0);  

    vec3 up = vec3(0.0, 1.0, 0.0);
    vec3 right = cross(up, normal);
    up = cross(normal, right);

    float sampleDelta = 0.025f;
    float nrSamples = 0.0f; 

    for (float phi = 0.0; phi < 2.0*PI; phi += sampleDelta)
    {
        for (float theta = 0.0; theta < 0.5*PI; theta += sampleDelta)
        {
            // Spherical to cartesian (in tangent space)
            vec3 tangentSample = vec3(sin(theta)*cos(phi), sin(theta)*sin(phi), cos(theta));
            
            // tangent space to world
            vec3 sampleVec = tangentSample.x*right + tangentSample.y*up + tangentSample.z*normal; 

            irradiance += texture(environmentMap, sampleVec).rgb*cos(theta)*sin(theta);
            nrSamples++;
        }
    }

    irradiance = PI*irradiance*(1.0/float(nrSamples));

    finalColor = vec4(irradiance, 1.0);
}