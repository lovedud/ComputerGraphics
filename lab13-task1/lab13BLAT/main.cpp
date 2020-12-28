#include<Windows.h>    
// first include Windows.h header file which is required    
#include<stdio.h>
#include <gl/glew.h>
#include<gl/GL.h>   // GL.h header file    
#include<gl/GLU.h> // GLU.h header file    
#include<gl/glut.h>  // glut.h header file from freeglut\include\GL folder    
#include<conio.h>
#include<math.h>
#include<string>
#include <vector>
#include <glm.hpp>
#include <string>
#include <sstream>
#include <fstream>
#include <iostream>
#include <cmath>
#include <iostream>
#include <Windows.h>
#include <gl/wglew.h>
#include "gl/freeglut.h"
#include <algorithm>
#include <functional>
#include <mat3x3.hpp>
#include <trigonometric.hpp>
#include <gtc/matrix_transform.hpp>
#include <gl/SOIL.h>
using namespace std;

float angle = 0.0f;//угол для поворота вокруг оси
float scale_x = 1.0f;//коэф для масштабирования по оси Х
float scale_y = 0.0f;//коэф для масштабирования по оси Y

//-------------------------- 0 - oneTexture, 1 - mix with Color, 2 - twoTextures
int mode = 2;

float factor = 0.4f;

string texPath1 = "stone.jpg"; //"ch.bmp";//"1.jpg";"3.jpg";
string texPath2 = "tex.jpg";//"2.jpg";//;;"5.jpg";"mandarin.bmp"
string vsPath = "vertex3.shader";
string fsPath1 = "fragment_oneText_forTask3.shader";
string fsPath2 = "fragment_mixColor_forTask3.shader";
string fsPath3 = "fragment_twoText_forTask3.shader";

GLuint Program;
GLint  Unif_matr;
GLint  Attrib_vertex;
GLint  Unif_color;
GLint  Unif_color_back;
GLint  Unif_cntLine;
GLuint vertexbuffer;
GLuint colorbuffer;
GLuint texturebuffer;
GLuint textureID;
GLuint textureID1;

void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

string loadFile(string path)
{
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0'))
		cout << fsS << endl;
	return fsS;
}

void LoadImage() {
	auto textureFlags = SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT;
	textureID1 = SOIL_load_OGL_texture(texPath1.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, textureFlags);
	textureID = SOIL_load_OGL_texture(texPath1.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, textureFlags);
}

void initShader()
{
	string _f = loadFile(vsPath);
	const char* vsSource = _f.c_str();

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	if (!mode) _f = loadFile(fsPath1);
	else if (mode == 1) _f = loadFile(fsPath2);
	else if (mode == 2) _f = loadFile(fsPath3);

	const char* fsSource = _f.c_str();

	fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &fsSource, NULL);
	glCompileShader(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok) { std::cout << "error attach shaders \n";   return; }

	checkOpenGLerror();
}

//Vertex Buffer Object для рендеринга
void initVBO() {
	GLuint VertexArrayID;
	glGenVertexArrays(1, &VertexArrayID);
	glBindVertexArray(VertexArrayID);
	static const GLfloat g_vertex_buffer_data[] = { 
		-1, -1, -1,
		1, -1, -1,
		1, 1, -1,

		-1, -1, -1,
		1, 1, -1,
		-1, 1, -1,

		1, -1, -1,
		1, -1, 1,
		1, 1, 1,

		1, -1, -1,
		1, 1, 1,
		1, 1, -1,

		1, -1, 1,
		-1, -1, 1,
		-1, 1, 1,

		1, -1, 1,
		-1, 1, 1,
		1, 1, 1,

		-1, -1, 1,
		-1, -1, -1,
		-1, 1, -1,

		-1, -1, 1,
		-1, 1, -1,
		-1, 1, 1,

		-1, -1, -1,
		-1, -1, 1,
		1, -1, 1,

		-1, -1, -1,
		1, -1, 1,
		1, -1, -1,

		-1, 1, -1,
		-1, 1, 1,
		1, 1, 1,

		-1, 1, -1,
		1, 1, 1,
		1, 1, -1
	};

	static const GLfloat g_color_buffer_data[] = {
		//

		1.0f , 0.5f , 1.0f ,
		1.0f , 0.5f , 0.5f ,
		0.5f , 0.5f , 1.0f ,

		1.0f , 0.5f , 1.0f ,
		0.5f , 0.5f , 1.0f ,
		0.0f , 1.0f , 0.0f , //Зеленый

		//

		1.0f , 0.5f , 0.5f ,
		0.0f , 1.0f , 1.0f ,
		1.0f , 0.0f , 1.0f ,

		1.0f , 0.5f , 0.5f ,
		1.0f , 0.0f , 1.0f ,
		0.5f , 0.5f , 1.0f ,

		//

		0.0f , 1.0f , 1.0f ,
		1.0f , 0.5f , 0.0f , //Оранжевый
		1.0f , 1.0f , 0.0f ,

		0.0f , 1.0f , 1.0f , //Голубой
		1.0f , 1.0f , 0.0f ,
		1.0f , 0.0f , 1.0f , //Фиолетовый

		//

		1.0f , 0.5f , 0.0f ,
		1.0f , 0.5f , 1.0f ,
		0.0f , 1.0f , 0.0f ,

		1.0f , 0.5f , 0.0f ,
		0.0f , 1.0f , 0.0f ,
		1.0f , 1.0f , 0.0f , //Желтый

		//

		1.0f , 0.5f , 1.0f ,
		1.0f , 0.5f , 0.0f ,
		0.0f , 1.0f , 1.0f ,

		1.0f , 0.5f , 1.0f , //Сиреневый
		0.0f , 1.0f , 1.0f ,
		1.0f , 0.5f , 0.5f , //Малиновый

		//

		0.0f , 1.0f , 0.0f ,
		1.0f , 1.0f , 0.0f ,
		1.0f , 0.0f , 1.0f ,

		0.0f , 1.0f , 0.0f ,
		1.0f , 0.0f , 1.0f ,
		0.5f , 0.5f , 1.0f , //Синий
	};

	static const GLfloat g_uv_buffer_data[] = {
		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1,

		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1,

		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1,

		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1,

		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1,

		0, 0,
		1, 0,
		1, 1,

		0, 0,
		1, 1,
		0, 1
	};
	//генерировать имена объектов буфера
	glGenBuffers(1, &vertexbuffer);
	//привязать именованный буферный объект
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	//создает и инициализирует хранилище данных буферного объекта
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_vertex_buffer_data), g_vertex_buffer_data, GL_STATIC_DRAW);

	glGenBuffers(1, &colorbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, colorbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_color_buffer_data), g_color_buffer_data, GL_STATIC_DRAW);

	glGenBuffers(1, &texturebuffer);
	glBindBuffer(GL_ARRAY_BUFFER, texturebuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_uv_buffer_data), g_uv_buffer_data, GL_STATIC_DRAW);
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

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glm::mat4 Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 100.0f);
	glm::mat4 View = glm::lookAt(glm::vec3(4, 3, 3), glm::vec3(0, 0, 0), glm::vec3(0, 1, 0));
	glm::mat4 Model = glm::mat4(1.0f);
	glm::mat4 MVP = Projection * View * Model;

	glUseProgram(Program);
	//Включить или отключить общий массив атрибутов вершин
	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	//определить массив общих данных атрибутов вершин
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, texturebuffer);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ARRAY_BUFFER, colorbuffer);
	glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	GLuint MatrixID = glGetUniformLocation(Program, "MVP");
	glUniformMatrix4fv(MatrixID, 1, GL_FALSE, &MVP[0][0]);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	glUniform1i(glGetUniformLocation(Program, "myTextureSampler"), 0);

	if (mode == 2)
	{
		//Указывает, какой текстурный блок сделать активным. Количество текстурных блоков зависит от реализации
		glActiveTexture(GL_TEXTURE1);
		glBindTexture(GL_TEXTURE_2D, textureID1);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

		glUniform1i(glGetUniformLocation(Program, "myTextureSampler1"), 1);

		glUniform1f(glGetUniformLocation(Program, "mix_f"), factor);
	}
	glDrawArrays(GL_TRIANGLES, 0, 12 * 4);

	glDisableVertexAttribArray(0);

	glFlush();

	glUseProgram(0);

	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(1000, 800);
	glutCreateWindow("Simple shaders");
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	GLenum glew_status = glewInit();

	initShader();

	LoadImage();

	initVBO();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render);
	glutMainLoop();

	freeShader();
}