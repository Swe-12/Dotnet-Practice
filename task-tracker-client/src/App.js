import React, { useEffect, useState } from 'react';
import api from './api';


function App() {
const [tasks, setTasks] = useState([]);
const [newTask, setNewTask] = useState('');


const loadTasks = async () => {
try {
const res = await api.get('/');
setTasks(res.data);
} catch (err) {
console.error('Failed to load tasks', err);
}
};


const addTask = async () => {
if (!newTask.trim()) return;
try {
await api.post('/', { title: newTask, description: '', isCompleted: false });
setNewTask('');
await loadTasks();
} catch (err) {
console.error('Failed to add task', err);
}
};

const toggleTask = async (task) => {
try {
await api.put(`/${task.id}`, { ...task, isCompleted: !task.isCompleted });
await loadTasks();
} catch (err) {
console.error('Failed to update task', err);
}
};


const deleteTask = async (id) => {
try {
await api.delete(`/${id}`);
await loadTasks();
} catch (err) {
console.error('Failed to delete task', err);
}
};


useEffect(() => { loadTasks(); }, []);


return (
<div style={{ padding: 20 }}>
<h2>🧾 Task Tracker</h2>
<input
placeholder="New Task..."
value={newTask}
onChange={(e) => setNewTask(e.target.value)}
/>
<button onClick={addTask}>Add</button>


<ul>
{tasks.map((t) => (
<li key={t.id} style={{ marginBottom: 8 }}>
<span
onClick={() => toggleTask(t)}
style={{ textDecoration: t.isCompleted ? 'line-through' : 'none', cursor: 'pointer' }}
>
{t.title}
</span>
<button style={{ marginLeft: 10 }} onClick={() => deleteTask(t.id)}>❌</button>
</li>
))}
</ul>
</div>
);
}


export default App;