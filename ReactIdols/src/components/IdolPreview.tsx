import type { CreateIdolRequest } from '../types';

interface IdolPreviewProps {
	value: CreateIdolRequest;
}

export function IdolPreview({ value }: IdolPreviewProps) {
	return (
		<div style={{ background: '#4752d3', padding: '20px', borderRadius: '12px', color: 'white' }}>
			<h2 style={{ marginTop: 0 }}>Idol Creation Preview</h2>
			<div style={{ display: 'flex', gap: 16, alignItems: 'center' }}>
				<div style={{ width: 96, height: 96, background: '#2229', borderRadius: 8, overflow: 'hidden', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
					{value.url ? (
						<img src={value.url} alt={value.name} style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
					) : (
						<span>No image</span>
					)}
				</div>
				<div>
					<div><strong>Name:</strong> {value.name || '—'}</div>
					<div><strong>Image URL:</strong> {value.url || '—'}</div>
				</div>
			</div>
		</div>
	);
}
