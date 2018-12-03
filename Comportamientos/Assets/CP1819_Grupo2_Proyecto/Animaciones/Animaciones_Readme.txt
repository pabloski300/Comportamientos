A_Camarero_Coger -> Al coger una bandeja de comida (o dejarla, velocidad -1). Sin loop (una vez).
A_Camarero_Idle -> Cuando el camarero está ocioso. Con loop.
A_Camarero_Interaccion -> Al interactuar con un cliente (tomar pedido o cobrar). Sin loop (una vez).
A_Camarero_Walking -> Movimiento. Con loop.

A_Cliente_Comer -> Al estar en el estado comer. Con loop (reproducir 3 - 4 veces aprox).
A_Cliente_Idle1 -> Al estar ocioso normal. Con loop.
A_Cliente_Idle2 -> De vez en cuando, alternar con Idle1. Sin loop (reproducir una vez).
A_Cliente_Idle3 -> Ídem. En general, reproducir idle1, y de vez en cuando meter el 2 y 3, pero siempre rellenando con el 1 por medio.
A_Cliente_IdleSilla -> Cuando está sentado en la silla. Con loop.
A_Cliente_LlamarCamarero -> Al llamar a un camarero para pedir o pagar. Sin loop (una vez).
A_Cliente_Walking -> Movimiento. Con loop.

A_Cocinero_Bandeja -> Llevar bandeja a la barra para que la recojan los camareros. Movimiento. Con loop.
A_Cocinero_Cocinar1 -> Al estar en estado cocinando. Con loop (reproducir 2 - 3 veces aprox).
A_Cocinero_Cocinar2 -> Ídem, alternar con la anterior.
A_Cocinero_Coger -> Al coger un objeto. Sin loop (una vez).
A_Cocinero_Fregar -> Al estar ocioso. Con loop.
A_Cocinero_Waling -> Movimiento. Con loop.

A_Limpieza_Barrer -> Al estar ocioso. Con loop.
A_Limpieza_Mesa -> Al limpiar una mesa. Con loop (reproducir un par de veces).
A_Limpieza_Walking -> Movimiento. Con loop.

A_Maitre_Idle -> Al estar ocioso. Con loop.
A_Maitre_Interaccion -> Al llegar un cliente, habla con él y le busca sitio. Sin loop (una vez).
A_Maitre_Walking -> Movimiento. Con loop.