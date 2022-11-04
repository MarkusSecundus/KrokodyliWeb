let repoPart = '/' + (await(await fetch('repo_info.json')).json()).GithubInfo.RepoName;
let baseHref = window.location.pathname.startsWith(repoName)
    ? repoPart
    : '/'
    ;

$('head  base').attr('href', baseHref);