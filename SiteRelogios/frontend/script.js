const API_URL = 'http://localhost:5000/relogios';
const form = document.getElementById('form-relogio');
const lista = document.getElementById('lista-relogios');

async function carregarRelogios() {
  lista.innerHTML = '';
  const res = await fetch(API_URL);
  const relogios = await res.json();

  relogios.forEach(r => {
    const div = document.createElement('div');
    div.className = 'relogio';
    div.innerHTML = `
      <span><strong>${r.marca}</strong> - ${r.modelo} - R$ ${r.preco.toFixed(2)}</span>
      <button onclick="editarRelogio(${r.id})">Editar</button>
      <button onclick="deletarRelogio(${r.id})">Excluir</button>
    `;
    lista.appendChild(div);
  });
}

form.addEventListener('submit', async e => {
  e.preventDefault();
  const marca = document.getElementById('marca').value;
  const modelo = document.getElementById('modelo').value;
  const preco = parseFloat(document.getElementById('preco').value);

  await fetch(API_URL, {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({ marca, modelo, preco })
  });

  form.reset();
  carregarRelogios();
});

async function deletarRelogio(id) {
  await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
  carregarRelogios();
}

async function editarRelogio(id) {
  const novaMarca = prompt('Nova marca:');
  const novoModelo = prompt('Novo modelo:');
  const novoPreco = prompt('Novo preço:');

  if (!novaMarca || !novoModelo || !novoPreco) return;

  await fetch(`${API_URL}/${id}`, {
    method: 'PUT',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
      id,
      marca: novaMarca,
      modelo: novoModelo,
      preco: parseFloat(novoPreco)
    })
  });

  carregarRelogios();
}

carregarRelogios();
