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

	li {
			display: flex;
			justify-content: space-between;
			align-items: center;
			outline: none;
        border-width: 1px;
        padding: 0.8rem 1.1rem;
        border: solid 1px #94A3B8;
	}

	li:first-child{                                
    -moz-border-radius-topleft: 15px;
    -moz-border-radius-topright: 15px;                

    border-top-left-radius: 15px;
    border-top-right-radius: 15px;
		
		border-bottom: 0px;
	}

	li:last-child{                                
    -moz-border-radius-bottomleft: 15px;
    -moz-border-radius-bottomright: 15px;                

    border-bottom-left-radius: 15px;
    border-bottom-right-radius: 15px;  
		
		border-top: 0px;
	}


</style>