import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import './style.css'

function Header() {
	return (
		<header>
			<div className="nav">
				<div className="brand">SpaceStation</div>
				<nav style={{ display: 'flex', gap: 12 as number }}>
					<Link to="/">Home</Link>
					<Link to="/news">News</Link>
					<Link to="/add">Add News</Link>
					<Link to="/links">Links</Link>
				</nav>
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
	return (
		<div className="container">
			<h1>News</h1>
			<p className="muted">Here will be the space news feed.</p>
		</div>
	)
}

function AddNews() {
	return (
		<div className="container">
			<h1>Add News</h1>
			<p className="muted">Form for publishing new entries will be here.</p>
		</div>
	)
}

function Links() {
	return (
		<div className="container">
			<h1>Links</h1>
			<p className="muted">Curated list of helpful resources.</p>
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


