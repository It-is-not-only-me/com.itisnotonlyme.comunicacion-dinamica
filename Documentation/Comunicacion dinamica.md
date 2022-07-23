# Documentacion
---

Tenemos las 4 interfaces: 
 * IPersona
 * IVinculo
 * IMensaje
 * IImportancia

De todos estos, solo la interfaz IImportancia no tiene una clase concentra que la implementa.

#### Diagrama
![Diagrama de interfaces](Diagrama%20de%20interfaces.png "Diagrama de interfaces")

### IPersona
---

Esta va a ser la representacion de cada npc en un juego, o cada entidad que sea capaz de comunicarse con alguien. Ya que tiene la forma de crear vinculos y transmitir mensajes.
Una opcion para modificacion es como hice en las pruebas donde creo un metodo especifico para ver los mensajes que tenga cada persona.

### IVinculo
---

Esto representaria las aristas de un grafo direccionado, donde los nodos son las personas. Estas aristas tiene un cierto peso, y esta determinado por la importancia.

### IMesnaje
---

Esta es la interfaz que represetaria cualquier cosa que se quiera transmitir en este grafo, lo pensado es mensajes, pero puede ser cualquier cosa y por eso es una de las clases que espero que mas se cambie. La intencion es simplemente representar la informacion que se tiene que transmitir con su importancia vinculada.

### IImportancia
--- 

Como vengo mencionando esta interfaz es la que vincula todo este sistema, y como varia tanto entre proyectos por ser algo totalmente subjetivo al juego que se este armando es el motivo por el cual es una interfaz sin implementacion concreta.

La idea de importancia puede ser representado con conceptos tan simples como un numero, o tan complejos como dicte el juego. Pero entre si tenga una forma de compararse. Esa es la idea de todo esto, poder comparar y evaluar si es necesario que se transmita cierta informacion entre personas.