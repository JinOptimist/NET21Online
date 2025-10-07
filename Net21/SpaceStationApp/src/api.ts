export type SpaceNews = {
	id: number
	title: string
	content: string
	imageUrl: string
}

const BASE_URL = 'https://localhost:7158'

export async function getSpaceNews(): Promise<SpaceNews[]> {
	const res = await fetch(`${BASE_URL}/GetSpaceNews`, { credentials: 'include' })
	if (!res.ok) {
		throw new Error(`Failed to load news: ${res.status}`)
	}
	return res.json()
}

export async function addSpaceNews(title: string, content: string, imageUrl: string): Promise<number> {
	const params = new URLSearchParams({ title, content, imageUrl })
	const res = await fetch(`${BASE_URL}/AddNews?${params.toString()}`, {
		method: 'GET', // Minimal API exposes AddNews as GET
		credentials: 'include'
	})
	if (!res.ok) {
		throw new Error(`Failed to add news: ${res.status}`)
	}
	return res.json()
}
