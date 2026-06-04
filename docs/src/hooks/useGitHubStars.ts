import { useEffect, useState } from 'react';

const CACHE_TTL = 5 * 60 * 1000;

export function useGitHubStars(repo: string): string {
  const cacheKey = `github_stars_${repo}`;
  const [stars, setStars] = useState<string>(() => {
    try {
      const cached = localStorage.getItem(cacheKey);
      if (cached) {
        const { value, ts } = JSON.parse(cached) as { value: string; ts: number };
        if (Date.now() - ts < CACHE_TTL) return value;
      }
    } catch {}
    return '';
  });

  useEffect(() => {
    try {
      const cached = localStorage.getItem(cacheKey);
      if (cached) {
        const { ts } = JSON.parse(cached) as { value: string; ts: number };
        if (Date.now() - ts < CACHE_TTL) return;
      }
    } catch {}

    fetch(`https://api.github.com/repos/${repo}`)
      .then(res => res.json())
      .then((data: { stargazers_count?: number }) => {
        if (data.stargazers_count !== undefined) {
          const value = String(data.stargazers_count);
          setStars(value);
          try {
            localStorage.setItem(cacheKey, JSON.stringify({ value, ts: Date.now() }));
          } catch {}
        }
      })
      .catch(() => {});
  }, [repo]);

  return stars;
}
