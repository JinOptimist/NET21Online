import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import './style.css'

function Layout() {
	return (
		<div>
			<nav style={{ display: 'flex', gap: 12 as number }}>
				<Link to="/">Home</Link>
				<Link to="/news">News</Link>
				<Link to="/add">Add News</Link>
				<Link to="/links">Links</Link>
			</nav>
		</div>
	)
}

function Home() {
	return <h1>Space Station Home</h1>
}

function News() {
	return <h1>News</h1>
}

function AddNews() {
	return <h1>Add News</h1>
}

function Links() {
	return <h1>Links</h1>
}

function NotFound() {
	return <h1>Not Found</h1>
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
		<Layout />
		<RouterProvider router={router} />
	</React.StrictMode>
)


