const apiUrl = '/api/users';

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

async function addUser() {
    const name = document.getElementById('userName').value;
    await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name })
    });
    document.getElementById('userName').value = '';
    loadUsers();
}

async function editUser(id, oldName) {
    const newName = prompt('Введите новое имя пользователя:', oldName);
    if (!newName) return;
    await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: newName })
    });
    loadUsers();
}

async function deleteUser(id) {
    if (!confirm('Удалить пользователя?')) return;
    await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
    loadUsers();
}

document.getElementById('addUserBtn').addEventListener('click', addUser);
loadUsers();
