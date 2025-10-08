export type Coffee = {
  id: number
  name: string
  imageUrl?: string
}

const BASE_URL = ''

const PRODUCTS_PATH = '/GetNameCoffe'
const CREATE_PATH_PREFIX = '/createCoffe'

async function apiFetch(input: string, init?: RequestInit): Promise<Response> {
  return fetch(`${BASE_URL}${input}`, { credentials: 'include', ...init })
}

async function ensureOk(res: Response): Promise<void> {
  if (!res.ok) {
    const text = await res.text()
    throw new Error(text || `Request failed with ${res.status}`)
  }
}

async function handle<T>(res: Response): Promise<T> {
  await ensureOk(res)
  if (res.status === 204) return undefined as unknown as T
  const contentType = res.headers.get('content-type') || ''
  return contentType.includes('application/json')
    ? res.json()
    : (await res.text()) as unknown as T
}

type CoffeeServer = {
  id?: number
  Id?: number
  name?: string
  Name?: string
  imageUrl?: string
  ImageUrl?: string
  url?: string
  Url?: string
}

function normalizeCoffee(server: CoffeeServer): Coffee {
  return {
    id:
      server.id ??
      server.Id ??
      Math.abs(
        ((server.name ?? server.Name ?? '') + (server.url ?? server.Url ?? '')).split('').reduce((a, c) => a + c.charCodeAt(0), 0)
      ),
    name: (server.Name ?? server.name ?? '').toString(),
    imageUrl: decodeURIComponent(server.ImageUrl ?? server.imageUrl ?? server.Url ?? server.url ?? ''),
  }
}

export async function fetchCoffees(): Promise<Coffee[]> {
  const res = await apiFetch(PRODUCTS_PATH)
  const data = await handle<CoffeeServer[] | CoffeeServer>(res)
  return (Array.isArray(data) ? data : [data]).map(normalizeCoffee)
}

export async function createCoffee(payload: { name: string; imageUrl: string }): Promise<void> {
  const name = encodeURIComponent(payload.name)
  const url = encodeURIComponent(payload.imageUrl ?? '')
  const res = await apiFetch(`${CREATE_PATH_PREFIX}/${name}/${url}`)
  await handle(res)
}
