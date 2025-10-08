export type Coffee = {
  id: number
  name: string
  imageUrl?: string
}

// Use dev proxy (configured in vite.config) during development.
const BASE_URL = ''

async function tryFirstSuccessful<T>(paths: string[], map: (r: Response) => Promise<T>): Promise<{ path: string; data: T }> {
  let lastError: unknown
  for (const path of paths) {
    try {
      const res = await fetch(`${BASE_URL}${path}`, { credentials: 'include' })
      if (res.ok) {
        const data = await map(res)
        return { path, data }
      }
      lastError = new Error(`HTTP ${res.status} @ ${path}`)
    } catch (e) {
      lastError = e
    }
  }
  throw lastError instanceof Error ? lastError : new Error('All endpoint candidates failed')
}

// Based on your Swagger:
// - List products: GET /GetNameCoffe (returns [{ id, name, url }])
// - Create coffee: GET /createCoffe/{name}/{url}
const PRODUCTS_PATH = '/GetNameCoffe'
const CREATE_PATH_PREFIX = '/createCoffe'

async function handle<T>(res: Response): Promise<T> {
  if (!res.ok) {
    const text = await res.text()
    throw new Error(text || `Request failed with ${res.status}`)
  }
  if (res.status === 204) return undefined as unknown as T
  const contentType = res.headers.get('content-type') || ''
  if (contentType.includes('application/json')) {
    return res.json() as Promise<T>
  }
  // Fallback: treat as text when server doesn't return JSON
  return (await res.text()) as unknown as T
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
    id: (server.id ?? server.Id ?? 0) || Math.abs(((server.name ?? server.Name ?? '') + (server.url ?? server.Url ?? '')).split('').reduce((a,c)=>a + c.charCodeAt(0),0)),
    name: (server.Name ?? server.name ?? '').toString(),
    imageUrl: decodeURIComponent(server.ImageUrl ?? server.imageUrl ?? server.Url ?? server.url ?? ''),
  }
}

export async function fetchCoffees(): Promise<Coffee[]> {
  const res = await fetch(`${BASE_URL}${PRODUCTS_PATH}`, { credentials: 'include' })
  const data = await handle<CoffeeServer[] | CoffeeServer>(res)
  const list = Array.isArray(data) ? data : [data]
  return list.map(normalizeCoffee)
}

export async function createCoffee(payload: { name: string; imageUrl: string }): Promise<void> {
  const name = encodeURIComponent(payload.name)
  const url = encodeURIComponent(payload.imageUrl ?? '')
  const res = await fetch(`${BASE_URL}${CREATE_PATH_PREFIX}/${name}/${url}`, { credentials: 'include' })
  if (!res.ok) {
    const text = await res.text()
    throw new Error(text || `Request failed with ${res.status}`)
  }
  // Endpoint returns non-JSON (e.g., plain text). Nothing to return; list will be reloaded separately.
}

