import type { CreateIdolRequest } from '../types';

interface IdolFormProps {
	value: CreateIdolRequest;
	onChange: (next: CreateIdolRequest) => void;
	onSubmit: () => void;
}

export function IdolForm({ value, onChange, onSubmit }: IdolFormProps) {
	return (
		<div style={{ background: '#ff8f2d', padding: '20px', borderRadius: '12px' }}>
			<h2 style={{ marginTop: 0 }}>Idol Creation</h2>
			<div style={{ display: 'grid', gap: 12 }}>
				<label>
					Name
					<input
						type="text"
						value={value.name}
						onChange={(e) => onChange({ ...value, name: e.target.value })}
						style={{ width: '100%', padding: 8 }}
					/>
				</label>
				<label>
					Image URL
					<input
						type="text"
						value={value.url}
						onChange={(e) => onChange({ ...value, url: e.target.value })}
						style={{ width: '100%', padding: 8 }}
					/>
				</label>
				<button onClick={onSubmit} style={{ padding: '10px 16px', borderRadius: 8 }} disabled={!value.name || !value.url}>Create</button>
			</div>
		</div>
	);
}
