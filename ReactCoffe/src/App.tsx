import { useEffect, useMemo, useState } from 'react'
import { Coffee, createCoffee, fetchCoffees } from './api/coffee'

type View = 'all' | 'add'

export default function App() {
  const [view, setView] = useState<View>('all')
  const [coffees, setCoffees] = useState<Coffee[]>([])
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<string | null>(null)

  const load = async () => {
    setLoading(true)
    setError(null)
    try {
      const data = await fetchCoffees()
      setCoffees(data)
    } catch (e) {
      setError((e as Error).message)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => { load() }, [])

  const SidebarButton = ({ label, active, onClick }: { label: string; active: boolean; onClick: () => void }) => (
    <button
      onClick={onClick}
      className={`w-full text-left px-5 py-4 border-b border-black/80 font-semibold ${active ? 'bg-black text-white' : 'hover:bg-black/5'}`}
    >
      {label}
    </button>
  )

  return (
    <div className="h-full grid" style={{ gridTemplateColumns: '240px 1fr' }}>
      <aside className="border-r border-black/80 flex flex-col">
        <div className="px-5 py-4 border-b border-black/80 text-2xl font-bold">CoffeAdmin</div>
        <SidebarButton label="All coffe" active={view==='all'} onClick={() => setView('all')} />
        <SidebarButton label="Add Coffe" active={view==='add'} onClick={() => setView('add')} />
        <div className="mt-auto">
          <button className="w-full px-5 py-4 border-t border-black/80 text-xl italic hover:bg-black/5">Exit</button>
        </div>
      </aside>

      <main className="p-6">
        {view === 'all' ? (
          <AllCoffee loading={loading} error={error} coffees={coffees} reload={load} />
        ) : (
          <AddCoffee onCreated={() => { setView('all'); load() }} />
        )}
      </main>
    </div>
  )
}

function AllCoffee({ loading, error, coffees, reload }: { loading: boolean; error: string | null; coffees: Coffee[]; reload: () => void }) {
  if (loading) return <div>Loading...</div>
  if (error) return <div className="text-red-600">{error}</div>
  if (!coffees.length) return <div className="space-y-4">
    <div>No coffee yet.</div>
    <button className="px-4 py-2 border rounded" onClick={reload}>Reload</button>
  </div>

  return (
    <div className="grid grid-cols-[repeat(auto-fill,minmax(220px,1fr))] gap-6">
      {coffees.map(c => (
        <article key={`${c.id}-${c.name}`} className="border rounded-lg overflow-hidden shadow-sm">
          {c.imageUrl ? (
            <img src={c.imageUrl} alt={c.name} className="w-full h-40 object-cover" />
          ) : (
            <div className="w-full h-40 bg-gray-100 flex items-center justify-center text-gray-500">No image</div>
          )}
          <div className="p-3 font-medium">{c.name}</div>
        </article>
      ))}
    </div>
  )
}

function AddCoffee({ onCreated }: { onCreated: () => void }) {
  const [name, setName] = useState('')
  const [imageUrl, setImageUrl] = useState('')
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const isValid = useMemo(() => name.trim().length > 0, [name])

  const submit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!isValid) return
    setSubmitting(true)
    setError(null)
    try {
      await createCoffee({ name: name.trim(), imageUrl: imageUrl.trim() || '' })
      setName('')
      setImageUrl('')
      onCreated()
    } catch (e) {
      setError((e as Error).message)
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <form className="max-w-lg space-y-4" onSubmit={submit}>
      <div>
        <label className="block text-sm font-medium mb-1">Name</label>
        <input value={name} onChange={e=>setName(e.target.value)} className="w-full border rounded px-3 py-2" placeholder="Latte" />
      </div>
      <div>
        <label className="block text-sm font-medium mb-1">Image URL</label>
        <input value={imageUrl} onChange={e=>setImageUrl(e.target.value)} className="w-full border rounded px-3 py-2" placeholder="https://..." />
      </div>
      {error && <div className="text-red-600">{error}</div>}
      <button disabled={!isValid || submitting} className="px-4 py-2 border rounded bg-black text-white disabled:opacity-50">
        {submitting ? 'Adding...' : 'Add Coffee'}
      </button>
    </form>
  )
}

