import { useEffect, useMemo, useState } from 'react';
import './App.css';
import { Header } from './components/Header';
import { IdolForm } from './components/IdolForm';
import { IdolPreview } from './components/IdolPreview';
import { IdolList } from './components/IdolList';
import { apiGet } from './api';
import type { CreateIdolRequest, Idol } from './types';

function App() {
	const [form, setForm] = useState<CreateIdolRequest>({ name: '', url: '' });
	const [idols, setIdols] = useState<Idol[]>([]);
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);
	
	async function loadIdols() {
		setLoading(true);
		setError(null);
		try {
			const data = await apiGet<Idol[]>('/GetIdols');
			setIdols(data);
		} catch (e) {
			setError((e as Error).message);
		} finally {
			setLoading(false);
		}
	}
	
	useEffect(() => {
		loadIdols();
	}, []);

	const canSubmit = useMemo(() => form.name.trim().length > 0 && form.url.trim().length > 0, [form]);

	async function handleCreate() {
		if (!canSubmit) return;
		try {
			// The API exposes creation via GET /createIdol/{name}/{url}
			const encodedName = encodeURIComponent(form.name);
			const encodedUrl = encodeURIComponent(form.url);
			await apiGet<number>(`/createIdol/${encodedName}/${encodedUrl}`);
			setForm({ name: '', url: '' });
			await loadIdols();
		} catch (e) {
			alert((e as Error).message);
		}
	}

	return (
		<div style={{ padding: 20, display: 'grid', gap: 20 }}>
			<Header />

			<div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20 }}>
				<IdolForm value={form} onChange={setForm} onSubmit={handleCreate} />
				<IdolPreview value={form} />
			</div>

			<IdolList items={idols} loading={loading} error={error} />
		</div>
	);
}

export default App;
