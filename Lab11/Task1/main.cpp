#include "SOIL.h"
#include "GL/glew.h"
#include "GL/freeglut.h"
#include <iostream>
#include <vector>
using namespace std;

GLuint floorTexture, carTexture, windowTexture;

float rotateX = 0, rotateY = 0, rotateZ = 0;

struct PointF {
	float x, y, z;

	PointF(float x, float y, float z) : x(x), y(y), z(z) { }
};

struct Light {
	PointF position;
	bool enabled = false;

	Light(float x, float y, float z) : position(PointF(x, y, z)) { }
};

struct Car {
	float positionX = 0;
	float positionZ = 0;
	bool enabledLight = false;
};

//позиции фонарей
vector<Light> lights = {
	Light(-170.0, 10.0, -170.0),
	Light(-170.0, 10.0, 170.0),
	Light(170.0, 10.0, -170.0),
	Light(170.0, 10.0, 170.0)
};

//объект машины, хранит ее позицию и состояние фар
Car car;

//загрузить текстуры
void loadTextures() {
	auto textureFlags = SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT;
	floorTexture = SOIL_load_OGL_texture("img/floor.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, textureFlags);
	carTexture = SOIL_load_OGL_texture("img/car.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, textureFlags);
	windowTexture = SOIL_load_OGL_texture("img/window.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, textureFlags);
}

void drawRoad() {
	glBindTexture(GL_TEXTURE_2D, floorTexture);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);
	glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);

	glEnable(GL_TEXTURE_2D);

	glBegin(GL_QUADS);
	glTexCoord2f(0.0, 0.0); glVertex3f(-200.0, 0.0, -200.0);
	glTexCoord2f(0.0, 1.0); glVertex3f(-200.0, 0.0, 200.0);
	glTexCoord2f(1.0, 1.0); glVertex3f(200.0, 0.0, 200.0);
	glTexCoord2f(1.0, 0.0); glVertex3f(200.0, 0.0, -200.0);
	glEnd();

	glDisable(GL_TEXTURE_2D);
}

void drawLights() {
	//Рисуем прожектор на камере
	GLfloat light0Pos[] = { 0, 200, 350, 1 };
	GLfloat light0Direction[] = { 0, -200, -350 };
	glLightfv(GL_LIGHT0, GL_POSITION, light0Pos);
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, light0Direction);
	glLightf(GL_LIGHT0, GL_SPOT_CUTOFF, 180);

	//Рисуем фонари
	for (int i = 0; i < lights.size(); i++) {

		PointF pos = lights[i].position;
		GLfloat lightPos[] = { 0, 0, 0, 1 };
		GLfloat lightColor[] = { 1, 1, 1, 1 };

		//Рисуем основания фонарей
		glColor3f(0.396, 0.263, 0.129);
		glPushMatrix();
		glTranslatef(pos.x, pos.y, pos.z);
		glutSolidCube(20);
		glPopMatrix();

		//Рисуем лампы в виде кубов
		glColor3f(1, 0.76, 0.678);
		glPushMatrix();
		glTranslatef(pos.x, pos.y + 15, pos.z);
		glutSolidCube(30);
		glPopMatrix();

		//навешиваем свет
		glPushMatrix();
		glLoadIdentity();
		glTranslatef(pos.x, pos.y + 26, pos.z);
		glLightfv(GL_LIGHT1 + i, GL_POSITION, lightPos);
		glLightfv(GL_LIGHT1 + i, GL_DIFFUSE, lightColor);
		glPopMatrix();
	}
}

void drawCar() {
	int carLength = 80, carHeight = 60, wheelRadius = 7;

	//колеса
	glColor3f(0.0, 0.0, 0.0);

	glPushMatrix();
	glTranslatef(car.positionX + wheelRadius - carLength / 2, wheelRadius + 1, car.positionZ + carHeight / 2);
	glutSolidTorus(3, wheelRadius, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(car.positionX + carLength / 2 - wheelRadius, wheelRadius + 1, car.positionZ + carHeight / 2);
	glutSolidTorus(3, wheelRadius, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(car.positionX + wheelRadius - carLength / 2, wheelRadius + 1, car.positionZ - carHeight / 2);
	glutSolidTorus(3, wheelRadius, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(car.positionX + carLength / 2 - wheelRadius, wheelRadius + 1, car.positionZ - carHeight / 2);
	glutSolidTorus(3, wheelRadius, 54, 54);
	glPopMatrix();

	//активируем текстуру машины
	glBindTexture(GL_TEXTURE_2D, carTexture);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);
	glEnable(GL_TEXTURE_2D);

	//нижняя часть автомобиля
	int clearance = 12;

	glColor3f(0.40, 0.35, 0.35);

	glBegin(GL_QUAD_STRIP);
	glVertex3f(car.positionX - carLength / 2, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(1, 0);

	glVertex3f(car.positionX - carLength / 2, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(0, 1);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 1);

	glVertex3f(car.positionX - carLength / 2, clearance, car.positionZ + carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, clearance, car.positionZ + carHeight / 2); glTexCoord2f(1, 0);

	glVertex3f(car.positionX - carLength / 2, clearance, car.positionZ - carHeight / 2);  glTexCoord2f(1, 1);
	glVertex3f(car.positionX + carLength / 2, clearance, car.positionZ - carHeight / 2);  glTexCoord2f(0, 1);

	glVertex3f(car.positionX - carLength / 2, 40.0, car.positionZ - carHeight / 2);  glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ - carHeight / 2);  glTexCoord2f(1, 0);
	glEnd();

	glBegin(GL_QUADS);
	glVertex3f(car.positionX - carLength / 2, 40.0, car.positionZ - carHeight / 2);  glTexCoord2f(0, 1);
	glVertex3f(car.positionX - carLength / 2, 40.0, car.positionZ + carHeight / 2);  glTexCoord2f(1, 1);
	glVertex3f(car.positionX - carLength / 2, clearance, car.positionZ + carHeight / 2);  glTexCoord2f(1, 0);
	glVertex3f(car.positionX - carLength / 2, clearance, car.positionZ - carHeight / 2);  glTexCoord2f(0, 0);
	glEnd();

	glBegin(GL_QUADS);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 1);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 1);
	glVertex3f(car.positionX + carLength / 2, clearance, car.positionZ + carHeight / 2); glTexCoord2f(1, 0);
	glVertex3f(car.positionX + carLength / 2, clearance, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glEnd();

	//кабина
	glBegin(GL_QUAD_STRIP);
	glVertex3f(car.positionX + carLength / 2 - 30, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(1, 0);

	glVertex3f(car.positionX + carLength / 2 - 30, 70.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 1);
	glVertex3f(car.positionX + carLength / 2, 70.0, car.positionZ + carHeight / 2); glTexCoord2f(0, 1);

	glVertex3f(car.positionX + carLength / 2 - 30, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 0);

	glVertex3f(car.positionX + carLength / 2 - 30, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 1);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(1, 1);

	glVertex3f(car.positionX + carLength / 2 - 30, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(1, 0);
	glVertex3f(car.positionX + carLength / 2, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glEnd();

	glBegin(GL_QUADS);
	glVertex3f(car.positionX + carLength / 2 - 30, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2 - 30, 70.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 0);
	glVertex3f(car.positionX + carLength / 2 - 30, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 1);
	glVertex3f(car.positionX + carLength / 2 - 30, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 1);
	glEnd();

	glDisable(GL_TEXTURE_2D);

	//активируем текстуру лобового стекла
	glBindTexture(GL_TEXTURE_2D, windowTexture);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);
	glEnable(GL_TEXTURE_2D);

	//лобовое стекло
	glColor3f(0.55, 0.9, 0.9);

	glBegin(GL_QUADS);
	glVertex3f(car.positionX + carLength / 2, 70.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 0);
	glVertex3f(car.positionX + carLength / 2, 70.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 0);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ + carHeight / 2); glTexCoord2f(1, 1);
	glVertex3f(car.positionX + carLength / 2, 40.0, car.positionZ - carHeight / 2); glTexCoord2f(0, 1);
	glEnd();

	glDisable(GL_TEXTURE_2D);

	GLfloat lightsColor[] = { 0.7, 0.7, 0.2, 1.0 };
	GLfloat lightsPosition[] = { 0, 0, 0, 1 };
	GLfloat lightsDirection[] = { 1, -0.1, 0 };

	GLfloat noLightMaterial[] = { 0, 0, 0, 1 };
	GLfloat lightMaterial[] = { 0.7, 0.7, 0.7, 0 };

	//фара 1
	glPushMatrix();
	glTranslatef(car.positionX + carLength / 2 + 5, 35, car.positionZ - carHeight / 2 + 10);

	glLightfv(GL_LIGHT6, GL_POSITION, lightsPosition);
	glLightfv(GL_LIGHT6, GL_SPOT_DIRECTION, lightsDirection);
	glLightf(GL_LIGHT6, GL_SPOT_CUTOFF, 30);
	glLightf(GL_LIGHT6, GL_SPOT_EXPONENT, 5);
	glLightfv(GL_LIGHT6, GL_DIFFUSE, lightsColor);

	if (car.enabledLight) glMaterialfv(GL_FRONT, GL_EMISSION, lightMaterial);
	else glMaterialfv(GL_FRONT, GL_EMISSION, noLightMaterial);

	glColor3f(0.7, 0.7, 0.2);
	glutSolidSphere(5, 48, 48);
	glPopMatrix();

	//фара 2
	glPushMatrix();
	glTranslatef(car.positionX + carLength / 2 + 5, 35, car.positionZ + carHeight / 2 - 10);
	
	glLightfv(GL_LIGHT7, GL_POSITION, lightsPosition);
	glLightfv(GL_LIGHT7, GL_SPOT_DIRECTION, lightsDirection);
	glLightf(GL_LIGHT7, GL_SPOT_CUTOFF, 30);
	glLightf(GL_LIGHT7, GL_SPOT_EXPONENT, 5);
	glLightfv(GL_LIGHT7, GL_DIFFUSE, lightsColor);

	if (car.enabledLight) glMaterialfv(GL_FRONT, GL_EMISSION, lightMaterial);
	else glMaterialfv(GL_FRONT, GL_EMISSION, noLightMaterial);

	glColor3f(0.7, 0.7, 0.2);
	glutSolidSphere(5, 48, 48);
	glPopMatrix();

	glMaterialfv(GL_FRONT, GL_EMISSION, noLightMaterial);
	glPopMatrix();
	glColor3f(1, 1, 1);
}

//Задать начальные параметры OpenGL
void init() {
	glClearColor(0.3f, 0.5f, 0.5f, 1.0f);
	glLoadIdentity();
	loadTextures();
	glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
	glEnable(GL_NORMALIZE);
	glEnable(GL_COLOR_MATERIAL);

	car.positionZ = 150;
}

//функция, вызываемая при изменении размера окна
void reshape(int width, int height) {
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(65.0f, width * 1.0f / height, 1.0f, 1000.0f);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

//отрисовка
void render() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	gluLookAt(0.0f, 200.0f, 350.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f); //местоположение наблюдателя и вектор наблюдения
	glRotatef(rotateX, 1.0, 0.0, 0.0);
	glRotatef(rotateY, 0.0, 1.0, 0.0);
	glRotatef(rotateZ, 0.0, 0.0, 1.0);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);
	
	//активируем прожектор на камере
	//glEnable(GL_LIGHT0);

	//активируем включенные фонари как источники света
	for (int i = 0; i < lights.size(); i++) {
		if (lights[i].enabled) 
			glEnable(GL_LIGHT1 + i);
	}

	//активируем фары
	if (car.enabledLight) {
		glEnable(GL_LIGHT6);
		glEnable(GL_LIGHT7);
	}

	drawRoad(); //рисуем дорогу
	drawLights(); //рисуем фонари
	drawCar(); //рисуем машину

	//деактивируем фары
	if (car.enabledLight) {
		glDisable(GL_LIGHT6);
		glDisable(GL_LIGHT7);
	}

	//деактивируем включенные фонари
	for (int i = 0; i < lights.size(); i++) {
		if (lights[i].enabled)
			glDisable(GL_LIGHT1 + i);
	}

	//деактивируем прожектор на камере
	//glDisable(GL_LIGHT0);

	glDisable(GL_LIGHTING);
	glDisable(GL_DEPTH_TEST);
	glFlush();
	glutSwapBuffers();
}

void keyboardCallback(unsigned char key, int x, int y) {
	switch (key) {
	case '1':
		if (lights.size() > 0) lights[0].enabled = !lights[0].enabled;
		break;
	case '2':
		if (lights.size() > 1) lights[1].enabled = !lights[1].enabled;
		break;
	case '3':
		if (lights.size() > 2) lights[2].enabled = !lights[2].enabled;
		break;
	case '4':
		if (lights.size() > 3) lights[3].enabled = !lights[3].enabled;
		break;
	case 'Q':
	case 'q':
		rotateY -= 5;
		break;
	case 'E':
	case 'e':
		rotateY += 5;
		break;
	case 'W':
	case 'w':
		car.positionX += 2;
		break;
	case 'S':
	case 's':
		car.positionX -= 2;
		break;
	case '0':
		car.enabledLight = !car.enabledLight;
	}
	glutPostRedisplay();
}

int main(int argc, char* argv[]) {
	//инициализация GLUT
    glutInit(&argc, argv);
    glutInitWindowPosition(50, 50);
    glutInitWindowSize(800, 600);
    glutCreateWindow("Car");
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);

	init();

	glutReshapeFunc(reshape);
	glutDisplayFunc(render);
	glutKeyboardFunc(keyboardCallback);

    glutMainLoop();
}
