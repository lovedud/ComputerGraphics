#include "Shader.cpp"
#define PI 3.1415926538

GLShader shader = GLShader();

GLuint Program;
GLint Attrib_vertex;
GLint Unif_color;
GLint Unif_angle;

float angle = 0.25;

double rotate_x = 0;
double rotate_y = 0;
double rotate_z = 0;

void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

void initShader()
{
	const char* vsSource =
		"attribute vec2 coord;\n"
		"uniform float angle;\n"
		"varying vec4 var_color; \n"
		"mat2 rot(in float a) {return mat2(cos(a), sin(a), -sin(a), cos(a));}\n"
		"void main() {\n"
		" vec2 pos = rot(3.14*angle)*coord;\n"
		" gl_Position = vec4(pos, 0, 1.0);\n"
		//" gl_Position = vec4(pos.x, 0, pos.y, 1.0);\n"
		" var_color = gl_Color;\n"
		"}\n";
	const char* fsSource =
		"varying vec4 var_color;\n"
		"void main() {\n"
		" gl_FragColor = var_color;\n"
		"}\n";

	shader.load(vsSource, fsSource);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}
	Attrib_vertex = shader.getAttribLocation("coord");
	Unif_angle = shader.getUniformLocation("angle");
	//Unif_color = shader.getAttribLocation("color");

	checkOpenGLerror();
}

float* getCoords()
{
	float m[16];
	glGetFloatv(GL_MODELVIEW_MATRIX, m);
	float x = m[0];
	float y = m[5];
	float z = m[10];
	float ans[3]{ x,y,z };
	return ans;
}

void Reshape(int width, int height) {
	glViewport(0, 0, width, height);
}

void specialKeys(int key, int x, int y) {
	switch (key) {
	case GLUT_KEY_UP: rotate_x += 5; angle += 0.1; break;
	case GLUT_KEY_DOWN: rotate_x -= 5; angle -= 0.1; break;
	case GLUT_KEY_RIGHT: rotate_y += 5; break;
	case GLUT_KEY_LEFT: rotate_y -= 5; break;
	case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
	case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
	}
	glutPostRedisplay();
}

void renderScene()
{
	glMatrixMode(GL_MODELVIEW);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();

	glRotatef(rotate_x, 1.0, 0.0, 0.0);
	glRotatef(rotate_y, 0.0, 1.0, 0.0);
	glRotatef(rotate_z, 0.0, 0.0, 1.0);

	shader.use();

	{
		glUniform1f(Unif_angle, angle);

		glBegin(GL_TRIANGLES);
		glColor3f(0, 1, 1);  glVertex2f(-0.5f, -0.5f);
		glColor3f(1, 0.2, 0);  glVertex2f(-0.5f, 0.5f);
		glColor3f(1, 0, 1);  glVertex2f(0.5f, 0.5f);
		//glColor3f(1, 1, 0);  glVertex2f(0.5f, -0.5f);
		glEnd();
	
	}


	glFlush();

	glUseProgram(0);
	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(600, 600);
	glutCreateWindow(" shaders ");

	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status)
	{
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
		return 1;
	}
	if (!GLEW_VERSION_2_0)
	{
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}

	initShader();

	glutDisplayFunc(renderScene);
	glutSpecialFunc(specialKeys);
	glutMainLoop();

	return 0;
}