#include <glew.h>
#include <freeglut.h>
#include <iostream>

GLuint Program;
GLint Attrib_vertex;
GLint Unif_color;
GLfloat Unif_angle;
float rot_angle = 0;

void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

void initShader1()
{
	const char* vsSource =
		"uniform float angle;\n"
		"attribute vec2 coord;\n"
		"mat2 rot(in float a) {return mat2(cos(a), sin(a), -sin(a), cos(a));}\n"
		"void main() {\n"
		"vec2 pos = rot(3.14*angle)*coord;\n"
		"gl_Position = vec4(pos, 0.0, 1.0);\n"
		"}\n";
	const char* fsSource =
		"uniform vec4 color;\n"
		"void main() {\n"
		" if (mod(gl_FragCoord.x, 30)<15.0) \n"
		" gl_FragColor = color;\n"
		" else\n"
		" gl_FragColor = vec4(1.0,1.0,1.0,0.0);\n"
		"}\n";

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	fShader = glCreateShader(GL_FRAGMENT_SHADER);

	glShaderSource(fShader, 1, &fsSource, NULL);

	glCompileShader(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	const char* attr_name = "coord";
	Attrib_vertex = glGetAttribLocation(Program, attr_name);
	if (Attrib_vertex == -1)
	{
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}

	const char* unif_name = "color";
	const char* unif_angle = "angle";
	Unif_color = glGetUniformLocation(Program, unif_name);
	Unif_angle = glGetUniformLocation(Program, unif_angle);
	if (Unif_color == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	if (Unif_angle == -1)
	{
		std::cout << "could not bind uniform " << unif_angle << std::endl;
		return;
	}
	checkOpenGLerror();
}

void initShader2()
{
	const char* vsSource =
		"uniform float angle;\n"
		"attribute vec2 coord;\n"
		"mat2 rot(in float a) {return mat2(cos(a), sin(a), -sin(a), cos(a));}\n"
		"void main() {\n"
		"vec2 pos = rot(3.14*angle)*coord;\n"
		"gl_Position = vec4(pos, 0.0, 1.0);\n"
		"}\n";
	const char* fsSource =
		"uniform vec4 color;\n"
		"void main() {\n"
		" if (mod(gl_FragCoord.x, 30)<15^mod(gl_FragCoord.y, 30)<15)\n"
		" gl_FragColor = color;\n"
		" else\n"
		" gl_FragColor = vec4(1.0,1.0,1.0,0.0);\n"
		"}\n";

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	fShader = glCreateShader(GL_FRAGMENT_SHADER);

	glShaderSource(fShader, 1, &fsSource, NULL);

	glCompileShader(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	const char* attr_name = "coord";
	Attrib_vertex = glGetAttribLocation(Program, attr_name);
	if (Attrib_vertex == -1)
	{
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}

	const char* unif_name = "color";
	const char* unif_angle = "angle";
	Unif_color = glGetUniformLocation(Program, unif_name);
	Unif_angle = glGetUniformLocation(Program, unif_angle);
	if (Unif_color == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	if (Unif_angle == -1)
	{
		std::cout << "could not bind uniform " << unif_angle << std::endl;
		return;
	}
	checkOpenGLerror();
}

void freeShader()
{
	glUseProgram(0);
	glDeleteProgram(Program);
}

void resizeWindow(int width, int height)
{
	glViewport(0, 0, width, height);
}

void specialKeys(int key, int x, int y)
{
	float angle = 0.1;
	switch (key)
	{
		case GLUT_KEY_RIGHT: rot_angle += angle; break;
		case GLUT_KEY_LEFT: rot_angle -= angle; break;
	}
	glutPostRedisplay();
}

void render1()
{
	glClear(GL_COLOR_BUFFER_BIT);
	glUseProgram(Program);
	static float red[4] = { 1.0f, 0.0f, 0.0f, 1.0f };
	static float green[4] = { 0.0f, 0.5f, 0.0f, 1.0f };
	static float blue[4] = { 0.0f, 0.0f, 0.7f, 1.0f };
	static float black[4] = { 0.0f, 0.0f, 0.0f, 1.0f };
	glUniform4fv(Unif_color, 1, black);
	glUniform1f(Unif_angle, rot_angle);
	glBegin(GL_QUADS);
	glVertex2f(-0.5f, -0.5f);
	glVertex2f(-0.5f, 0.5f);
	glVertex2f(0.5f, 0.5f);
	glVertex2f(0.5f, -0.5f);
	glEnd();
	glFlush();
	glUseProgram(0);
	checkOpenGLerror();
	glutSwapBuffers();
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(600, 600);
	glutCreateWindow("Simple shaders");
	glClearColor(0.7, 0.7, 0, 0);

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

	initShader1();
	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render1);
	glutSpecialFunc(specialKeys);
	glutMainLoop();
	freeShader();
}
