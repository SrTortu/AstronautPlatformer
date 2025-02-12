# Super2DProject

Change Log:

Player: 

	*Ahora el jugador puede atravezar plataformas desde abajo
	* Ahora el jugador puede correr ( usando las teclas de movimiento)
	* Se cambia "Volar" por "saltar" (tecla space)
	* el jugador mira a la posicion relativa del mouse
	* Se implementa pistola de ganchos
	* Se añade alerta sonora y visual cuando el player no tenga suficiente energia para saltar
	* Saltar ahora tiene sonido
	* Se añade animacion para los efectos negativos de los items
	
Pistola: lanza un gancho (click izquierdo) el cual se sujetara de una plataforma si esta a 10m o menos
	
	* La pistola del jugador ahora mira en posicion relativa al mouse
	* Al saltar el gancho es eliminado de la plataforma
	* Click derecho elimina el gancho (no tiene utilidad)
	* Si la plataforma desaparece el gancho tambien
	* Si la plataforma se mueve el gancho mantendra su posicion sobre la plataforma
 	* Se añade sonido de disparo	

Plataformas: se añaden plataformas especiales (Nivel 3, 4 y 5)
	
	*Las plataformas del nivel 3 desaparecen cada cierto tiempo y vuelven a aparecer (intervalo aleatorio)
	*Las plataformas del nivel 4 se mueven en el eje X de un punto A a un punto B
		-Se añade la posibilidad de pararse sobre las plataformas de nivel 4 y que el player se mueva con la plataforma
	*Las plataformas del nivel 5 tambien se mueven pero tienen un material especifico con poca friccion.

Escenario: 

	*Se añaden luces a los niveles 
	*Se cambia la musica de "InGame" (Ahora corresponde a "Inner station" de Metal Slug)
	*Se añade musica de intro al Main Menu (Intro de Contra)
 
Items: Se cambian todos los items y su funcionalidad, Sprite, particulas y sonidos
	
		-Item Bomba: Hace que el jugador tenga un impulso negativo en el eje Y. Y tambien paraliza al player por 1seg
			     ademas, si el jugador se encuentra enganchado a una plataforma se eliminara el gancho
		-Item skull: Disminiye el ratio de regeneracion de energia en -0.1. Tambien paraliza al jugador	1seg.
		-Item Watter: Aumenta el ratio de regeneracion de energia en 0.1
	*Los items son elminados correctamente al tocar el suelo		     


