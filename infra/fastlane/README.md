fastlane documentation
----

# Installation

Installation for MacOS

```sh
brew install rbenv ruby-build
rbenv init
echo 'eval "$(rbenv init - zsh)"' >> ~/.zshrc
rbenv install -l
rbenv install 3.1.2
rbenv local 3.1.2
rbenv global 3.1.2
#restart you shell
ruby --version
gem install bundler
vi Gemfile
#Add below lines
#  source "https://rubygems.org"
#  gem "fastlane"
bundle update
```

For _fastlane_ installation instructions, see [Installing _fastlane_](https://docs.fastlane.tools/#installing-fastlane)

