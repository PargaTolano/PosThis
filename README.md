# PosThis
_Proyecto Final de Programacion Web II_
	
	“PosThis” es una aplicación que desea que los usuarios tengan la capacidad de transmitir y consumir cantidades 
	digeribles de información sobre el mundo. Funciona como una red social donde el usuario puede interactuar con 
	seguidores y sus post.

## Integrantes ✒️
	José Antonio Parga Tolano  - 1808868
	Esteban Barbosa Martínez   - 1735087
	Yareli Guevara Villalpando - 1805427
	Valdemar Botello Jasso     - 1542845

	
## Carpetas 📋
Carpetas generales

1. **SQL**
```
	* Tablas.sql
	
	Tablas de la base de datos de la aplicación, 
	aquí se crean las tablas con sus atributos y 
	la relación entre las mismas. Se necesita
	para poder correr los endpoints.
	
	* TablasDeploy.sql
	
	Creación de la base de datos de la aplicación,
	se crean las mismas tablas que con el script anterior.
	Esta es necesaria para el uso de la base de datos remota
	en el deploy del host.
	
```

2. **BACKEND**

```
* Rest API PWII:
	Contiene lo necesario para el desarrollo web del programa. Contiene:
	
	Classes:
		Clases referenciales de las tablas de la 
		base de datos con atributos y llaves.

	Controllers:
		Controladores del programa. Contiene los
		Endpoints para interactuar con la base de
		datos.

	Models:
		Interpreta los registros de la base de datos.
		Inicializa los modelos de la base de datos
		para que los datos tengan respectivas descrip.
				
	Properties:
		Default de la estructura del .NET, modifica los settings.
				
		obj:
			Caché del proyecto
				
	     	Rest API PWII.sln
			Es el programa tal cuál.
```
3. **FRONTEND** 

Carpeta para la interfaz de la aplicación 


* public:

```
Para recursos que se usan en toda la página web, y la página en si, template de React.
```

* src: 

```
Tiene los scripts y hojas de estilo de la interfaz. Contiene subcarpetas:
```

- ***_helpers:***
Contiene helpers, es decir, funciones no complejas que realizan una
sola acción que se puede repetir a lo largo de la aplicación.

- ***_services:***
Servicios utilizados para la carga y envío de datos por medio de 
RXJS. Actualmente solo necesitado para la autenticación.

- ***_utils:***
Utilidades para simplificar secciones de código similar a los helpers. 
Aquí también pude haber atributos no funcionales.

- ***_api:***
Contiene la configuración de las peticiones de la API, incluyendo
el URL. Además de las peticiones a la API realizadas a través de FETCH API.
			
- ***assets:*** 
Recursos multimedia para la aplicación. Incluye logos e imágenes de fondo.
				
- ***components:***
Contiene componentes funcionales para la aplicación de REACT.

- ***_hooks:***
Incluye hooks personalizados. Reducen la cantidad de código utilizado dentro
de un componente funcional.

- ***_model:***
Contiene los modelos que se utilizan para enviar datos al WebService
de una manera estructurada.

- ***mock:***
Utilizada para contener datos de prueba para la aplicación de REACT.



## Instalación 🔧

- ***Repositorio:***
```
Para ejecutar el programa, es necesario clonar el repositorio
de github desde la siguiente liga:  
```
https://github.com/PargaTolano/PosThis.git
```
Desde la consola con el comando git clone, podrá hacerse del
código para el desarrollo web del programa. Si opta por clonar el repositorio
de esta manera es necesario que ponga en consola la siguiente línea de código:

	$ git clone https://github.com/PargaTolano/PosThis.git
	
	
Si posée git hub desktop, solo basta con pegar la liga anterior en la parte de
"Current repository", "Add", "clonar repositorio" y seleccionar el lugar donde
se estará guardando el contenido del mismo.
```

- ***Backend:***
```
Para instalar los elementos necesarios para la parte del backend, basta
con ejecutar el script de ***Tablas.sql*** para la creación de la base de datos.
```

- ***Frontend:***
```
Se debe ubicar en el directorio en que tiene instalado el proyecto, para después
navegar a la carpeta de FRONTEND. Desde aqui, debe ejecutar la siguiente instrucción
desde la consola:
		npm install
```

## Ejecución ⚙️

_Para poder tener la base de datos, es necesario que posea SQL Server Managment Studio._

- ***Base de Datos:***
```
	Acceda a la carpeta con el nombre que le haya dado al contenido del repositorio 
	en su ordenador, ejemplo si lo ha nombrado "PostThis" seleccione la carpeta, 
	y acontinuación la carpeta llamada "sql", dentro podrá encontrar el script 
	de la base de datos llamado "Tablas.sql", proceda a abrirla.

	Al iniciar la conexión con sql con el administrador o su usuario, note que hay 
	un botón llamada "Execute", sin más presionelo para que el script ejecute y cree
	las tablas de la misma base, si no hay ningún problema, proceda a seleccionar en 
	el panel izquierdo (Object Explorer) la opción "refresh" (F5), y dentro del 
	apartado de "Database" aparecerá la base de datos con el nombre de "PosThis",
	donde se podrá ver las tablas y sus atributos para su futuro uso.
```

- ***Backend:***
```
	Para esta parte, basta con dar click en el botón superior de nombre EJECUTAR, dentro
	de VISUAL STUDIO 2019.
```


- ***Frontend:***
```
	En cuanto a esta parte, se debe ubicar en el directorio de la carpeta FRONTEND dentro
	de la carpeta del proyecto principal. A partir de aqui, debe ejecutar la siguiente instrucción
	desde la consola:
			npm start
```

## Hosting :desktop_computer:

Ligas de los repositorios necesarios para probar la aplicación montada en un servicio de HOST.

- ***Frontend:***

	https://posthis.herokuapp.com/


- ***Backend:***

	http://pargatolano-001-site1.dtempurl.com/

- ***BUILD:***
```
Repositorio secundario, de aquí se obtienen los archivos para subirlo al host, build de react.
```
https://github.com/PargaTolano/PosThis-React-app
