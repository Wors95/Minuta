const API_URL = 'http://localhost:5123/relogios';
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
      <button onclick="editarRelogio('${r.id}')">Editar</button>
      <button onclick="deletarRelogio('${r.id}')">Excluir</button>
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

const modal = document.getElementById('modal-editar');
const formEditar = document.getElementById('form-editar');
const cancelarBtn = document.getElementById('cancelar-edicao');

// Abrir o modal com dados preenchidos
async function editarRelogio(id) {
  const res = await fetch(`${API_URL}/${id}`);
  const r = await res.json();

  document.getElementById('edit-id').value = r.id;
  document.getElementById('edit-marca').value = r.marca;
  document.getElementById('edit-modelo').value = r.modelo;
  document.getElementById('edit-preco').value = r.preco;

  modal.style.display = 'flex';
}

// Cancelar edição
cancelarBtn.addEventListener('click', () => {
  modal.style.display = 'none';
});

// Salvar alterações
formEditar.addEventListener('submit', async e => {
  e.preventDefault();
  const id = document.getElementById('edit-id').value;
  const marca = document.getElementById('edit-marca').value;
  const modelo = document.getElementById('edit-modelo').value;
  const preco = parseFloat(document.getElementById('edit-preco').value);

  await fetch(`${API_URL}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ id, marca, modelo, preco })
  });

  modal.style.display = 'none';
  carregarRelogios();
});


carregarRelogios();
