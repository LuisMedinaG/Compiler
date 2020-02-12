/*
 * 	NOTAS:
 *  token   int     identificador
 *          x       identificador
 *  token   ;       punto y coma
 *          &       error
 *          #       error
 */

#include <string>

void func(std::string cadena) {
  int i = 0;
  int estado = 0;
  std::string tipo;

  while (/* no es fin de cadena */ true) {
    std::string token = "";
    estado = 0;

    while (/* no es fin de cadena ó */ estado != 20) {
      switch (estado) {
        case 0:
          if (/*cadena[i] == '' || */ cadena[i] == '\n' || cadena[i] == '\t') {
            i++;
          } else if (/* cadena[i] es letra o _ */ true) {
          } else if (/* cadena[i] es digito */ true) {
          } else if (/* cadena[i] == &*/ true) {
          } else if (/* cadena[i] == ';' */ true) {
          } else {
            estado = 20;
          }
          break;
        case 1:
          if (/* cadena[i] es letra, numero o guion bajo */) {
            token += cadena[i];
            i++;
          } else {
            estado = 20;
            tipo = "identificador";
          }
          break;
        case 4:
          if (cadena[i] == '&') {
            estado = 20;
            tipo = "and";
            token += cadena[i];
            i++;
          } else {
            estado = 20;
            tipo = "error";
          }
          break;
      }
    }
  }
}

int maint() { return 0; }