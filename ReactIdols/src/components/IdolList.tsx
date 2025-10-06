import type { Idol } from '../types';

interface IdolListProps {
	items: Idol[];
	loading?: boolean;
	error?: string | null;
}

export function IdolList({ items, loading, error }: IdolListProps) {
	return (
		<section style={{ background: '#22b24c', padding: '20px', borderRadius: '12px', color: 'white', minHeight: 260 }}>
			<h2 style={{ marginTop: 0 }}>Idols List</h2>
			{loading && <div>Loading...</div>}
			{error && <div style={{ color: '#000', background: '#fff3cd', padding: 8, borderRadius: 8 }}>{error}</div>}
			<div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(220px, 1fr))', gap: 12 }}>
				{items.map((idol) => (
					<article key={idol.id} style={{ background: 'rgba(0,0,0,0.15)', padding: 12, borderRadius: 8 }}>
						<div style={{ display: 'flex', gap: 12 }}>
							<div style={{ width: 64, height: 64, background: '#0003', borderRadius: 6, overflow: 'hidden' }}>
								{idol.url && <img src={idol.url} alt={idol.name ?? ''} style={{ width: '100%', height: '100%', objectFit: 'cover' }} />}
							</div>
							<div>
								<div style={{ fontWeight: 700 }}>{idol.name ?? 'Unnamed'}</div>
							</div>
						</div>
					</article>
				))}
			</div>
		</section>
	);
}
