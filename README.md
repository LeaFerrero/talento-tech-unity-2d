# Proyecto Final: Talento Tech Unity 2D

Esta es la entrega del proyecto final para el curso de Talento Tech Unity 2D, desarrollado a lo largo del cuatrimestre.

## Descripción General
El juego es un *bullet heaven* de supervivencia espacial inspirado en *Vampire Survivors*. El jugador controla una nave para enfrentarse a oleadas de enemigos, recolectar experiencia y mejorar su armamento mientras intenta sobrevivir el mayor tiempo posible.

## Controles
*   **Movimiento:** El jugador controla la nave utilizando el mouse. Al mantener presionado el **clic izquierdo**, la nave se desplaza continuamente en dirección al puntero.

## Interfaz (HUD)
Durante la partida, la pantalla muestra la siguiente información en tiempo real:
*   **Tiempo transcurrido:** Ubicado en la parte superior derecha.
*   **Salud (HP):** Vida actual de la nave.
*   **Armamento:** Armas actualmente equipadas y su ubicación en la nave.
*   **Progreso:** Barra de experiencia y nivel actual del jugador.

## Mecánicas de Juego

### Sistema de Disparo
El ataque de la nave es completamente automático. Las armas equipadas se disparan de forma continua sin requerir interacción manual del jugador.

### Sistema de Oleadas (Enemigos)
Las naves enemigas aparecen por oleadas. Pueden ser destruidas por los disparos del jugador o al colisionar directamente contra su nave (lo cual le causa daño al jugador). El ciclo de dificultad escala basándose en el tiempo de supervivencia y se reinicia cada 2 minutos:

| Tiempo | Tipo de Nave | Características |
| :--- | :--- | :--- |
| **0:00 - 0:59** | ⚪ Blancas | Naves básicas, las más débiles. |
| **1:00 - 1:59** | 🟡 Amarillas | Mayor velocidad, vida y daño. |
| **2:00 en adelante** | 🔵 Azules | Las naves más fuertes del ciclo. |
*(Nota: Luego del minuto 2, el patrón de oleadas se reinicia desde el principio).*

### Progresión y Power-Ups
Al destruir naves enemigas, estas sueltan ítems de experiencia. Llenar la barra de experiencia permite al jugador subir de nivel. Al hacerlo, el juego se pausa y presenta un menú con **dos power-ups aleatorios** (de un total de cuatro posibles) para elegir:
1.  **Mejora de Salud:** Restaura o aumenta la vida de la nave.
2.  **Nuevas Armas:** Cada una con estadísticas distintas. Al elegir un arma, el jugador decide en qué ranura de la nave equiparla: **Frontal, Izquierda, Derecha o Trasera**. La elección se refleja inmediatamente en el HUD.

### Entorno (Asteroides)
El escenario incluye asteroides destructibles que actúan como obstáculos y recursos:
*   Causan daño por colisión tanto al jugador como a las naves enemigas.
*   Al ser destruidos, tienen probabilidad de soltar ítems de curación.

## Fin del Juego
El juego finaliza si la vida del jugador llega a 0. Se despliega una pantalla de *Game Over* con opciones para **Reiniciar la partida** o **Volver al Menú Principal**.

##  Autor

**Leandro Raul Ferrero**
* [GitHub](https://github.com/leaFerrero)
* Proyecto Final - Talento Tech (Unity 2D)