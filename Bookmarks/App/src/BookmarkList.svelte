<script>
    export let bookmarks;
</script>

<main>
    <ul class="list-group">
		{#each bookmarks as bookmark}
		<li>{bookmark.uri}</li>
		{/each}
	</ul>
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

</style>