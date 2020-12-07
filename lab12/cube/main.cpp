#include <glew.h>
#include <freeglut.h>

#include "GLShader.cpp"

#include <iostream>
//! Переменные с индентификаторами ID
//! ID шейдерной программы
GLuint Program;
//! ID юниформ переменной цвета
GLint Unif_color;

GLShader shader;
GLShader shaderVBO;
GLuint VBO_vertex;
GLuint VBO_color;
GLuint VBO_indices;
GLuint VBO_Attrib_vertex;
GLuint VBO_Attrib_color;
GLuint VBO_Unif_matrix;

GLuint Attrib_vertex;
GLuint Attrib_color;
GLuint Unif_matrix;

int indicies_count;
bool VBO_on = true;

double rotate_x = 45;
double rotate_y = 45;
double rotate_z = 0;
//! Проверка ошибок OpenGL, если есть то вывод в консоль тип ошибки
void checkOpenGLerror()
{
	const GLubyte* kek;
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
	{
		kek = gluErrorString(errCode);
		std::cout << kek << std::endl;
	}

}
void initVBO()
{
	glGenBuffers(1, &VBO_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	float vertices[8][3] = {
		{-0.5f, -0.5f, -0.5f},
		{0.5f, -0.5f, -0.5f},
		{0.5f, 0.5f, -0.5f},
		{-0.5f, 0.5f, -0.5f},
		{-0.5f, -0.5f, 0.5f},
		{0.5f, -0.5f, 0.5f},
		{0.5f, 0.5f, 0.5f},
		{-0.5f, 0.5f, 0.5f},
	};

	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices,
		GL_STATIC_DRAW);

	glGenBuffers(1, &VBO_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	float color[8][3] = {
		{1.0f, 0.5f, 1.0f},
		{1.0f, 0.5f, 0.5f},
		{0.5f, 0.5f, 1.0f},
		{0.0f, 1.0f, 1.0f},
		{-1.0f, 0.0f, 1.0f},
		{1.0f, 1.0f, 0.0f},
		{1.0f, 0.0f, 1.0f},
		{0.0f, 1.0f, 1.0f},
	};
	glBufferData(GL_ARRAY_BUFFER, sizeof(color), color,
		GL_STATIC_DRAW);

	glGenBuffers(1, &VBO_indices);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_indices);
	GLint indices[] = {
		0 , 4 , 5 , 0 , 5 , 1 ,
		1 , 5 , 6 , 1 , 6 , 2 ,
		2 , 6 , 7 , 2 , 7 , 3 ,
		3 , 7 , 4 , 3 , 4 , 0 ,
		4 , 7 , 6 , 4 , 6 , 5 ,
		3 , 0 , 1 , 3 , 1 , 2
	};
	indicies_count = sizeof(indices) / sizeof(indices[0]);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices,
		GL_STATIC_DRAW);
	checkOpenGLerror();
}

void freeVBO()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO_vertex);
	glDeleteBuffers(1, &VBO_color);
	glDeleteBuffers(1, &VBO_indices);
}
//! Инициализация шейдеров для варианта без VBO
void initShaderWithoutVBO()
{
	//! Исходный код шейдеров
	const char* vertex_Source =
		"attribute vec3 coord;\n"
		"uniform mat4 matrix;\n"
		"out vec4 var_color;\n"
		"void main() {\n"
		"gl_Position = matrix * vec4(coord, 1.0);\n"
		"var_color = gl_Color;\n"
		"}\n";
	const char* fragment_Source =
		"in vec4 var_color;\n"
		"void main() {\n"
		" gl_FragColor = var_color;\n"
		"}\n";

	shader = GLShader();
	shader.load(vertex_Source, fragment_Source);
	Program = shader.getIDProgram();

	Attrib_vertex = shader.getAttribLocation("coord");
	Unif_matrix = shader.getUniformLocation("matrix");
}

//! Инициализация шейдеров для варианта с VBO
void initShader()
{
	//! Исходный код шейдеров
	const char* vertex_Source =
		"attribute vec3 coord;\n"
		"attribute vec3 color;\n"
		"varying vec3 var_color;\n"
		"uniform mat4 matrix;\n"
		"void main() {\n"
		"gl_Position = matrix * vec4(coord, 1.0) ;\n"
		"var_color = color;\n"
		"}\n";
	const char* fragment_Source =
		"uniform vec4 color;\n"
		"varying vec3 var_color;\n"
		"void main() {\n"
		" gl_FragColor = vec4(var_color, 1.0);\n"
		"}\n";

	shaderVBO = GLShader();
	shaderVBO.load(vertex_Source, fragment_Source);
	Program = shaderVBO.getIDProgram();

	VBO_Attrib_vertex = shaderVBO.getAttribLocation("coord");

	VBO_Attrib_color = shaderVBO.getAttribLocation("color");

	VBO_Unif_matrix = shaderVBO.getUniformLocation("matrix");
	initVBO();
}
void resizeWindow(int width, int height)
{
	glViewport(0, 0, width, height);
}
//! Отрисовка 
void render2()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glLoadIdentity();
	glRotatef(rotate_x, 1, 0, 0);
	glRotatef(rotate_y, 0, 1, 0);
	glRotatef(rotate_z, 0, 0, 1);
	float m[16];
	glGetFloatv(GL_MODELVIEW_MATRIX, m);
	float matr[4][4];
	for (int i = 0; i < 4; i++)
	{
		for (int j = 0; j < 4; j++)
		{
			matr[i][j] = m[i * 4 + j];
		}
	}
	if (VBO_on)
	{
		shaderVBO.use();
		glUniformMatrix4fv(Unif_matrix, 1, GL_FALSE, &matr[0][0]);

		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_indices);

		glEnableVertexAttribArray(VBO_Attrib_vertex);
		glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
		glVertexAttribPointer(VBO_Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 0, 0);


		glEnableVertexAttribArray(VBO_Attrib_color);
		glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
		glVertexAttribPointer(VBO_Attrib_color, 3, GL_FLOAT, GL_FALSE, 0, 0);

		glDrawElements(GL_TRIANGLES, indicies_count, GL_UNSIGNED_INT, 0);

		glDisableVertexAttribArray(VBO_Attrib_vertex);
		glDisableVertexAttribArray(VBO_Attrib_color);
		shaderVBO.off();
	}
	else
	{
		shader.use();
		glUniformMatrix4fv(Unif_matrix, 1, GL_FALSE, &matr[0][0]);

		glBegin(GL_QUADS);
		glColor3f(1.0f, 0.5f, 1.0f);
		glVertex3f(-0.5f, -0.5f, -0.5f);//1
		glColor3f(1.0f, 0.5f, 0.5f);
		glVertex3f(-0.5f, 0.5f, -0.5f);//2
		glColor3f(0.5f, 0.5f, 1.0f);
		glVertex3f(0.5f, 0.5f, -0.5f);//3
		glColor3f(0.0f, 1.0f, 1.0f);
		glVertex3f(0.5f, -0.5f, -0.5f);//4

		glColor3f(1.0f, 0.0f, 1.0f);
		glVertex3f(-0.5f, -0.5f, 0.5f);//5
		glColor3f(1.0f, 1.0f, 0.0f);
		glVertex3f(-0.5f, 0.5f, 0.5f);//6
		glColor3f(1.0f, 0.5f, 0.5f);
		glVertex3f(-0.5f, 0.5f, -0.5f);//2
		glColor3f(1.0f, 0.5f, 1.0f);
		glVertex3f(-0.5f, -0.5f, -0.5f);//1

		glColor3f(1.0f, 0.0f, 0.5f);
		glVertex3f(0.5f, -0.5f, 0.5f);//7
		glColor3f(0.5f, 1.0f, 1.0f);
		glVertex3f(0.5f, 0.5f, 0.5f);//8
		glColor3f(1.0f, 1.0f, 0.0f);
		glVertex3f(-0.5f, 0.5f, 0.5f);//6
		glColor3f(1.0f, 0.0f, 1.0f);
		glVertex3f(-0.5f, -0.5f, 0.5f);//5

		glColor3f(0.0f, 1.0f, 1.0f);
		glVertex3f(0.5f, -0.5f, -0.5f);//4
		glColor3f(0.5f, 0.5f, 1.0f);
		glVertex3f(0.5f, 0.5f, -0.5f);//3
		glColor3f(0.5f, 1.0f, 1.0f);
		glVertex3f(0.5f, 0.5f, 0.5f);//8
		glColor3f(1.0f, 0.0f, 0.5f);
		glVertex3f(0.5f, -0.5f, 0.5f);//7

		glColor3f(1.0f, 0.5f, 1.0f);
		glVertex3f(-0.5f, -0.5f, -0.5f);//1
		glColor3f(0.0f, 1.0f, 1.0f);
		glVertex3f(0.5f, -0.5f, -0.5f);//4
		glColor3f(1.0f, 0.0f, 0.5f);
		glVertex3f(0.5f, -0.5f, 0.5f);//7
		glColor3f(1.0f, 0.0f, 1.0f);
		glVertex3f(-0.5f, -0.5f, 0.5f);//5

		glColor3f(1.0f, 0.5f, 0.5f);
		glVertex3f(-0.5f, 0.5f, -0.5f);//2
		glColor3f(1.0f, 1.0f, 0.0f);
		glVertex3f(-0.5f, 0.5f, 0.5f);//6
		glColor3f(0.5f, 1.0f, 1.0f);
		glVertex3f(0.5f, 0.5f, 0.5f);//8
		glColor3f(0.5f, 0.5f, 1.0f);
		glVertex3f(0.5f, 0.5f, -0.5f);//3

		glEnd();
		shader.off();
	}

	checkOpenGLerror();
	glFlush();
	glutSwapBuffers();
}
void specialKeys(int key, int x, int y) {

	switch (key) {
	case GLUT_KEY_UP: rotate_x += 5; break;
	case GLUT_KEY_DOWN: rotate_x -= 5; break;
	case GLUT_KEY_RIGHT: rotate_y += 5; break;
	case GLUT_KEY_LEFT: rotate_y -= 5; break;
	case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
	case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
	case GLUT_KEY_F4:
		if (VBO_on)
		{
			VBO_on = false;
			Program = shader.getIDProgram();
		}
		else
		{
			VBO_on = true;
			Program = shaderVBO.getIDProgram();
		}
		break;
	}
	glutPostRedisplay();
}
int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(600, 600);
	glutCreateWindow("Simple shaders");
	glClearColor(0, 0, 1, 0);
	//! Обязательно перед инициализацией шейдеров
	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status)
	{
		//! GLEW не проинициализировалась
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
		return 1;
	}
	//! Проверяем доступность OpenGL 2.0
	if (!GLEW_VERSION_2_0)
	{
		//! OpenGl 2.0 оказалась не доступна
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}
	
	glEnable(GL_DEPTH_TEST);
	//! Инициализация шейдеров
	initShaderWithoutVBO();
	initShader();
	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render2);
	glutSpecialFunc(specialKeys);
	glutMainLoop();
	freeVBO();
}
