attribute vec3 coord;//стандартный vector, содержащий в себе n значений типа float
uniform mat4 matrix;
uniform mat4 MVP;
void main() {
	gl_Position = MVP * matrix * vec4(coord, 1.7);
}