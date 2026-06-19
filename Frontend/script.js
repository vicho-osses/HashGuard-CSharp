const formulario = document.getElementById('registerForm');
const contenedorMensaje = document.querySelector('.message');

formulario.addEventListener('submit', async (event) => {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const datosUsuario = {
        username: username,
        email: email,
        password: password
    };

    const urlAPI = 'https://localhost:7048/api/auth/register'; 

    try {
        // Enviamos los datos por la red en formato JSON
        const respuesta = await fetch(urlAPI, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(datosUsuario) // Convertimos el objeto a texto JSON
        });

        // Si el servidor responde un código 200 o 201 (Todo OK)
        if (respuesta.ok) {
            // Mostramos el recuadro verde que estilizamos en CSS
            contenedorMensaje.textContent = "¡Usuario registrado con éxito en el sistema seguro!";
            contenedorMensaje.className = "message success"; // Le quita la clase 'hidden' y activa el verde
            formulario.reset(); // Limpia las cajitas del formulario
        } else {
            // Si el backend nos rebota (ej: el correo ya existe)
            contenedorMensaje.textContent = "Error en el registro. Verifica los datos.";
            contenedorMensaje.className = "message error"; // (Podemos agregar estilo rojo después)
        }

    } catch (error) {
        // Si el backend está apagado o la URL está mala
        console.error("Error de conexión:", error);
        contenedorMensaje.textContent = "No se pudo conectar con el servidor backend.";
        contenedorMensaje.className = "message error";
    }
});