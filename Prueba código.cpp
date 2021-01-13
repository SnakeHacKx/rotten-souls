#include <iostream>
#include <stdlib.h>
#include <time.h>

using namespace std;

int main()
{
	int numero, dato, contador = 0;

	//hola wey

	srand(time(NULL)); //generar un numero aleatorio entre 1-100

	dato = 1 + rand() % (101 - 1);

	do
    {
		cout << "Digite un numero: ";
		cin >> numero;

		if(numero > dato)
			cout << "Es un numero menor\n";

		else if(numero < dato)
			cout << "Es un numero mayor\n";

		contador++;

	}while(numero != dato);

	cout << "\n¡FELICIDADES ADIVINO EL NUMERO!" << endl;
	cout << "Numero de intentos: " << contador << endl;

	system("pause");
	return 0;
}
