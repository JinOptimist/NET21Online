import React, { useEffect, useState } from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import './style.css'
import { getSpaceNews, addSpaceNews, type SpaceNews } from './api'

function ThemeToggle() {
	const [theme, setTheme] = useState<'dark' | 'light'>('dark')

	useEffect(() => {
		document.documentElement.setAttribute('data-theme', theme)
	}, [theme])

	return (
		<button
			className="btn-primary"
			onClick={() => setTheme(t => (t === 'dark' ? 'light' : 'dark'))}
			aria-label="Toggle theme"
		>
			{theme === 'dark' ? 'Switch to Light' : 'Switch to Dark'}
		</button>
	)
}

function Header() {
	return (
		<header>
			<div className="nav">
				<div className="brand">🛰️ SpaceStation</div>
				<nav style={{ display: 'flex', gap: 12 as number }}>
					<Link to="/">Home</Link>
					<Link to="/news">News</Link>
					<Link to="/add">Add News</Link>
					<Link to="/links">Links</Link>
				</nav>
				<div style={{ marginLeft: 'auto' }}>
					<ThemeToggle />
				</div>
			</div>
		</header>
	)
}

function Home() {
	return (
		<div className="container">
			<h1>Space Station Home</h1>
			<p className="muted">Welcome aboard. Choose a section to continue.</p>
			<div className="grid">
				<div className="card">
					<h3>Latest News</h3>
					<p className="muted">Fresh updates from orbit and beyond.</p>
					<Link className="btn-primary" to="/news">Open News</Link>
				</div>
				<div className="card">
					<h3>Add News</h3>
					<p className="muted">Publish a new discovery or station update.</p>
					<Link className="btn-primary" to="/add">Create</Link>
				</div>
				<div className="card">
					<h3>Links</h3>
					<p className="muted">Useful references and resources.</p>
					<Link className="btn-primary" to="/links">Explore</Link>
				</div>
			</div>
		</div>
	)
}

function News() {
	const [items, setItems] = useState<SpaceNews[]>([])
	const [loading, setLoading] = useState(true)
	const [error, setError] = useState<string | null>(null)

	useEffect(() => {
		getSpaceNews()
			.then(setItems)
			.catch(e => setError(e.message))
			.finally(() => setLoading(false))
	}, [])

	return (
		<div className="container">
			<h1>News</h1>
			{loading && <p className="muted">Loading…</p>}
			{error && <p className="muted">Error: {error}</p>}
			<div className="grid">
				{items.map(n => (
					<div className="card" key={n.id}>
						<h3>{n.title}</h3>
						<p className="muted">{n.content}</p>
						{n.imageUrl && <img src={n.imageUrl} alt="news" style={{ width: '100%', borderRadius: 8 }} />}
					</div>
				))}
			</div>
		</div>
	)
}

function AddNews() {
	const [submitting, setSubmitting] = useState(false)
	const [message, setMessage] = useState<string | null>(null)

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault()
		const formData = new FormData(e.currentTarget)
		const title = String(formData.get('title') || '')
		const imageUrl = String(formData.get('imageUrl') || '')
		const content = String(formData.get('content') || '')
		setSubmitting(true)
		setMessage(null)
		try {
			const id = await addSpaceNews(title, content, imageUrl)
			setMessage(`Published. id=${id}`)
			e.currentTarget.reset()
		} catch (e: any) {
			setMessage(`Error: ${e.message}`)
		} finally {
			setSubmitting(false)
		}
	}

	return (
		<div className="container">
			<h1>Add News</h1>
			<form className="form" onSubmit={handleSubmit}>
				<label className="field">
					<span>Title</span>
					<input name="title" required placeholder="Enter title" />
				</label>
				<label className="field">
					<span>Image URL</span>
					<input name="imageUrl" type="url" placeholder="https://..." />
				</label>
				<label className="field">
					<span>Content</span>
					<textarea name="content" rows={6} required placeholder="Your news content..." />
				</label>
				<div style={{ display: 'flex', gap: 12 as number }}>
					<button className="btn-primary" type="submit" disabled={submitting}>{submitting ? 'Publishing…' : 'Publish'}</button>
					<button type="reset" disabled={submitting}>Clear</button>
				</div>
				{message && <p className="muted">{message}</p>}
			</form>
		</div>
	)
}

function Links() {
	return (
		<div className="container">
			<h1>Links</h1>
			<p className="muted">Curated list of helpful resources.</p>
			<ul>
				<li><a href="https://www.nasa.gov/" target="_blank">NASA</a></li>
				<li><a href="https://www.esa.int/" target="_blank">ESA</a></li>
				<li><a href="https://www.spacex.com/" target="_blank">SpaceX</a></li>
			</ul>
		</div>
	)
}

function NotFound() {
	return (
		<div className="container">
			<h1>Not Found</h1>
			<p className="muted">The page you’re looking for doesn’t exist.</p>
		</div>
	)
}

const router = createBrowserRouter([
	{ path: '/', element: <Home /> },
	{ path: '/news', element: <News /> },
	{ path: '/add', element: <AddNews /> },
	{ path: '/links', element: <Links /> },
	{ path: '*', element: <NotFound /> }
])

ReactDOM.createRoot(document.getElementById('app') as HTMLElement).render(
	<React.StrictMode>
		<Header />
		<RouterProvider router={router} />
	</React.StrictMode>
)


