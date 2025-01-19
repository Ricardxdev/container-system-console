## Gestionar contenedores: registrar información relevante de cada contenedor. 
- Código Contenedor: Es el identificador único del contenedor (Código alfanumérico 
de 3 letras seguidas de 5 números). 
- Tipo: Describe el tipo de contenedor según sus dimensiones y características: 
    - 20 pies (TEU - Twenty-foot Equivalent Unit) es un Contenedor estándar de 
20 pies de largo.
    - 40 pies (FEU - Forty-foot Equivalent Unit) es un Contenedor estándar de 40 
pies de largo.
    - High Cube (HC) es un Contenedores de altura superior al estándar.
    - Reefer (RF): Contenedores refrigerados para transportar mercancías que 
requieren control de temperatura.
    - Open Top (OT): Contenedores con techo abierto para cargas que sobresalen por 
la parte superior.
    - Flat Rack (FR): Contenedores sin paredes laterales ni techo, utilizados para 
cargas de gran tamaño o formas irregulares.
    - Tank Container (TC): Contenedores cisterna para transportar líquidos o gases a 
granel.
- Estado: Indica el estado actual del contenedor en el ciclo de envío: 
    - En puerto (En depósito/Almacenado): El contenedor se encuentra en una 
terminal portuaria o un depósito.
    - Cargado: El contenedor ha sido cargado en un medio de transporte (barco, 
tren, camión).
    - En tránsito: El contenedor está siendo transportado.
    - Descargado: El contenedor ha sido descargado del medio de transporte.
    - Entregado: El contenedor ha llegado a su destino final y ha sido entregado 
al destinatario.
Página 2 de 6
    - En reparación/Mantenimiento: El contenedor está fuera de servicio 
temporalmente.
    - Vacío: El contenedor está disponible para ser cargado.
- Peso Máximo: Peso máximo que puede transportar el contenedor. 
- Tara: Peso del contenedor vacío.
## Gestionar clientes: Almacenar información de los clientes propietarios de la mercancía:
- IdCliente: código alfanumérico de 3 letras seguidas de 5 números como 
Identificador único del cliente.
- Nombre: Nombre o razón social del cliente.
- Tipo de Cliente: clasificación del cliente ("Importador", "Exportador", "Mayorista", 
"Minorista").
- Dirección: dirección física del cliente.
- Teléfono: número de teléfono del cliente.
## Gestionar artículos: Almacenar información de los artículos:
- IdArticulo: código alfanumérico de 3 letras seguidas de 5 números.
- Descripción: descripción del artículo (ej. "Televisor Samsung 55 pulgadas", "Cajas 
de zapatos").
- Peso: peso unitario del artículo.
- Cantidad: cantidad de unidades de este artículo dentro del contenedor.
- Unidad Medida de Cantidad: medida del peso (ej. kg, lb).
- Valor Unitario: valor unitario del artículo ($).
### Gestionar la propiedad de la mercancía dentro de los contenedores
- **Permitir que los clientes sean propietarios de los artículos dentro de los contenedores.** (DONE)
### Gestionar el inventario detallado dentro de cada contenedor
- **Registrar los artículos específicos que contiene cada contenedor.** (DONE)
### Realizar consultas eficientes (Container Search Engine)
**Permitir consultar la información de los contenedores, los clientes y los artículos:**
- Obtener un listado de los clientes que tienen mercancía en un contenedor 
específico. (DONE)
- Obtener un listado de los artículos contenidos en un contenedor específico. (DONE)
- Obtener un listado de los contenedores que contienen un artículo específico. (DONE)
- Obtener el historial de contenedores que se han manejado con un cliente. (DONE)
- Obtener un listado de contenedores con su contenido y propietarios.
- Obtener un listado de clientes con los contenedores que utilizan.
- Obtener un listado de los contenedores según un estado especifico. (DONE)
- Obtener un listado de los contenedores vacíos. (DONE)
- Obtener un listado de los contenedores en mantenimiento. (DONE)
- Obtener el valor total de la mercancía dado un contenedor. (DONE)