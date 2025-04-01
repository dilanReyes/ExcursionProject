document.getElementById('formSeleccion').addEventListener('submit', async function (e) {
    e.preventDefault();

    const submitButton = document.getElementById('submitButton');
    submitButton.disabled = true;

    const caloriasMinimas = document.getElementById('caloriasMinimas').value;
    const pesoMaximo = document.getElementById('pesoMaximo').value;
    const listaElementos = document.getElementById('listaElementos');
    const mensajeDiv = document.getElementById('mensaje');

    listaElementos.innerHTML = '';
    mensajeDiv.innerHTML = '';

    try {
        const response = await fetch('http://localhost:5193/api/excursion/seleccionar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                CaloriasMinimas: parseInt(caloriasMinimas),
                PesoMaximo: parseInt(pesoMaximo)
            })
        });

        const data = await response.json();

        mensajeDiv.innerHTML = `<div class="alert ${data.elementos.length > 0 ? 'alert-success' : 'alert-warning'}">${data.mensaje}</div>`;

        if (data.elementos.length > 0) {
            data.elementos.forEach(elemento => {
                const li = document.createElement('li');
                li.classList.add('list-group-item');
                li.textContent = `${elemento.nombre} - Peso: ${elemento.peso} kg, Calorías: ${elemento.calorias}`;
                listaElementos.appendChild(li);
            });
        }
    } catch (error) {
        mensajeDiv.innerHTML = '<div class="alert alert-danger">Error al conectar con el servidor.</div>';
    } finally {
        submitButton.disabled = false;
    }
});
