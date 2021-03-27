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

			Classes:

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
	de github desde la siguiente liga: https://github.com/PargaTolano/PosThis.git

	Desde la consola con el comando git clone, podrá hacerse del
	código para el desarrollo web del programa. Si opta por clonar el repositorio
	de esta manera es necesario que ponga en consola la siguiente línea de código:

	$ git clone https://github.com/PargaTolano/PosThis.git
	
	
	Si posé git hub desktop, solo basta con pegar la liga anterior en la parte de
	"Current repository", "Add", "clonar repositorio" y seleccionar el lugar donde
	se estará guardando el contenido del mismo.

	
	Instrucciones de Ejecución de Script de SQL: 

	Para poder tener la base de datos, es necesario que posea SQL
	Server Managment Studio. Acceda a la carpeta con el nombre que le haya dado a 
	el contenido del repositorio en su ordenador, ejemplo si lo ha nombrado "PostThis"
	seleccione la carpeta, y acontinuación la carpeta llamada "sql", dentro podrá
	encontrar el script de la base de datos, proceda a abrirla.

	Al iniciar la conexión con sql con el administrador o su usuario, note que hay 
	un botón llamada "Execute", sin más presionelo para que el script ejecute y cree
	las tablas de la misma base, si no hay ningún problema, proceda a seleccionar en 
	el panel izquierdo (Object Explorer) la opción "refresh" (F5), y dentro del 
	apartado de "Database" aparecerá la base de datos con el nombre de "PosThis",
	donde se podrá ver las tablas y sus atributos para su futuro uso. 





 