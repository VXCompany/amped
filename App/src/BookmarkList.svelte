<script>
    export let bookmarks;
</script>

<main>
    <ul>
		{#each bookmarks as bookmark}
		<li>{bookmark.uri}</li>
		{/each}
	</ul>
</main>

<style></style>