const apiUrl = '/api/users';
const modal = document.getElementById('userModal');
const modalTitle = document.getElementById('modalTitle');
const saveBtn = document.getElementById('saveUserBtn');
const userNameInput = document.getElementById('userName');

let editUserId = null;

function openModal(edit = false, name = '') {
    modal.style.display = 'flex';
    modalTitle.textContent = edit ? 'Редактировать пользователя' : 'Добавить пользователя';
    userNameInput.value = name;
}

function closeModal() {
    modal.style.display = 'none';
    editUserId = null;
    userNameInput.value = '';
}

document.getElementById('addUserBtn').addEventListener('click', () => openModal());

window.onclick = function (event) {
    if (event.target == modal) closeModal();
}

async function loadUsers() {
    const res = await fetch(apiUrl);
    const users = await res.json();
    const tbody = document.querySelector('#usersTable tbody');
    tbody.innerHTML = '';
    users.forEach(u => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${u.id}</td>
            <td>${u.name}</td>
            <td>
                <button onclick="editUser('${u.id}', '${u.name}')">Редактировать</button>
                <button onclick="deleteUser('${u.id}')">Удалить</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

async function addUser(name) {
    await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name })
    });
    loadUsers();
}

async function updateUser(id, name) {
    await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name })
    });
    loadUsers();
}

async function deleteUser(id) {
    if (!confirm('Удалить пользователя?')) return;
    await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
    loadUsers();
}

function editUser(id, name) {
    editUserId = id;
    openModal(true, name);
}

saveBtn.addEventListener('click', () => {
    const name = userNameInput.value.trim();
    if (!name) return alert('Введите имя');
    if (editUserId) updateUser(editUserId, name);
    else addUser(name);
    closeModal();
});

loadUsers();
