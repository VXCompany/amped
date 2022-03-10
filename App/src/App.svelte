<script>
import { onMount } from "svelte";
import BookmarkList from './BookmarkList.svelte';
import CreateBookmark from './CreateBookmark.svelte';

	export let name;
	let bookmarks = []

	onMount(async () => {
		const res = await fetch("http://localhost:5001/api/Bookmark/all");
		bookmarks = await res.json();
	})
</script>

<main>
	<h1>Hello {name}!</h1>
	<p>Ik wil een front-end met leuke kleurtjes!</p>

	<BookmarkList bookmarks={bookmarks} />
	<CreateBookmark/>
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>