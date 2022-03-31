<script>
import { onMount } from 'svelte';
import BookmarkList from './BookmarkList.svelte';
import CreateBookmark from './CreateBookmark.svelte';

	export let name;
	let bookmarks = []

	onMount(async () => {
		await fetchBookmarks();
	})

	async function fetchBookmarks() {
		const res = await fetch(AMPED_API_URL + '/Bookmark/all');
		bookmarks = await res.json();
	}
</script>

<header>
  <a href="." class="logo-wrap">
    <img src="logo.png" alt="" height="50">
  </a>
  <ul>
    <li>
      <a href="/">Home</a>
    </li>
  </ul>
  <span class="btn-wrap">
    <a href="https://github.com/VXCompany/amped" class="btn">Join us</a>
  </span>
</header>

<main>
	<h1>Hello {name}!</h1>
	<p>Ik wil een front-end met leuke kleurtjes!</p>

	<BookmarkList bookmarks={bookmarks} />
	<CreateBookmark on:created={fetchBookmarks} />
</main>

<style>
	* {
		box-sizing: border-box;
	}

	:global(body) {
		background-color: #dadde1;
		min-width: 320px;
	}

	header {
		background-color: white;
		border-radius: 15px;
		padding: 0.8rem 1.4rem;
	}

	.btn {
		background-color: #5E44CA;
		color: white;
		padding: 0.5rem 1.2rem;
		border-radius: 15px;
		text-decoration: none;
	}

	ul {
		list-style-type: none;
		padding: 0;
		margin: 0 -1.4rem;
		text-align: center;
	}

	ul li {
		display: inline-block;
		margin: 0 1.4rem;
	}

	ul li a {
		color: #5E44CA;
		text-decoration: none;
	}

	header {
		display: flex;
		justify-content: space-between;
		align-items: center;
	}

	.logo-wrap, .btn-wrap {
		flex: 1;
	}

	.btn-wrap {
		text-align: right;
	}

	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		color: #5E44CA;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}

	@media screen and (max-width: 767px) {
		header {
			flex-wrap: wrap;
		}
		ul {
			order: 3;
			flex: 100%;
			margin-top: 1rem;
		}
	}
</style>