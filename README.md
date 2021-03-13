# PosThis
Proyecto Final de Programacion Web II

Integrantes:
	José Antonio Parga Tolano  - 1808868
	Esteban Barbosa Martínez   - 1735087
	Yareli Guevara Villalpando - 1805427
	Valdemar Botello Jasso     - 1542845

Descripción de la Aplicación:
	“PosThis” es una aplicación que desea que los usuarios tengan
	la capacidad de transmitir y consumir cantidades digeribles 
	de información sobre el mundo. Funciona como una red social
	donde el usuario puede interactuar con seguidores y sus post.

Carpetas:
	Carpetas generales

	sql: 
	    Tablas.sql  
	     	Tablas de la base de datos de la aplicación, 
		aquí se crean las tablas con sus atributos y 
		la relación entre las mismas.

	backend: 
	     Rest API PWII
		Contiene lo necesario para el desarrollo web del
		programa. Contien:
			Controllers:
			Controladores del programa

			Models:
			Interpreta los registros de la base de datos.
			Inicializa los modelos de la base de datos
			para que los datos tengan respectivas descrip.
				
			Properties:
			Default de la estructura del .NET, modifica
			los settings.
				
			obj:
			Caché del proyecto
				
	     Rest API PWII.sln
		Es el programa tal cuál.

	frontend: 
	Carpeta para la interfaz de la aplicación 
			public:
			Carpeta es para recursos que se usan en toda
			la página web, y la página en si, template de
			React.

			src: 
			tiene los scripts y hojas de estilo de la 
			interfaz. Contiene subcarpetas:
				API:
				JS para la correcta ejecución de la
				API
			
				assets: 
				Contiene un botón para placeholder
				
				components:
				JS para el funcionamiento del Login

				hooks:
				JS que modifican la estructura de 
				react para que cargue la página.
		
Instrucciones de instalación:

	Para ejecutar el programa, es necesario clonar el repositorio
	de github: https://github.com/PargaTolano/PosThis.git

	Desde la consola con el comando git clone, podrá hacerse del
	código para el desarrollo web del programa.
	