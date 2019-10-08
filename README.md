# ORGA PROYECTO 1 : PLOTTER SERIAL 📌  
## Explicación de Puerto Serial 🔧  

El puerto serial funciona para la comunicación entre dispositivos electrónicos.  

Quién realiza esta comunicación de datos, es conocido como  
**UART**  
-Universal  
-Asynchronous  
-Receiver  
-Transmitter  

La comunicación se realiza por medio de paquetes de bits, conocidos como tramas.  

El primer bit de envío, será el bit menos significativo del paquete.  
Para reconocer una trama, primero se ingresa un bit de inicio por un periodo de tiempo llamado, *tiempo de bit.*  

Este tiempo de bit es el tiempo en que una señal *se mantiene por el transmisor* de información del puerto serie de igual forma se puede decir que es la velocidad de envío (**BAUD-RATE**)  
Y cada dato de envío se tardará este tiempo de bit en salir del transmisor de datos.  

Por ultimo se le enviará un **bit de stop** que terminará la trama (paquete) de envio, provisionalmente se identificará con un 1 lógico.

## Construido con 🛠️

* [Visual Studio] - Entorno de Desarrollo Integrado (IDE) utilizado.
* [C#] - Lenguaje de Programación utilizado.

## Autores ✒️

* **Cristian Suy** - *[Coordinador] 201700918* - [CSuy](https://github.com/CSuy)
* **Yelstin de León** - *201602836* - [airton47](https://github.com/airton47)
* **Ricardo Pérez** - *201700524* - [Ricardo16X](https://github.com/Ricardo16X)
* **Byron Gómez** - *201700544* - [ByrCas](https://github.com/ByrCas)
* **Diego Méndez** - *201712680* - [DiegoALbert6799](https://github.com/DiegoALbert6799)
